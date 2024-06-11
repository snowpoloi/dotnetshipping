using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShippingCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class OffersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OffersController> _logger;

    public OffersController(ApplicationDbContext context, ILogger<OffersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var offers = await _context.Offers.Include(o => o.Carrier).ToListAsync();
        return View(offers);
    }

    public IActionResult Create()
    {
        ViewData["Carriers"] = new SelectList(_context.Carriers, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Offer offer, string selectedPostalCodes)
    {
        if (offer == null)
        {
            _logger.LogError("Offer object is null");
            return View(offer);
        }

        var postalCodeIds = new List<int>();
        if (!string.IsNullOrEmpty(selectedPostalCodes))
        {
            postalCodeIds = JsonConvert.DeserializeObject<List<int>>(selectedPostalCodes);
        }

        _logger.LogInformation("Starting creation of a new offer with data: {@Offer}, selected postal codes: {@SelectedPostalCodes}", offer, postalCodeIds);

        // Remove validation for unused fields based on the offer type
        if (offer.OfferType == "Weight")
        {
            ModelState.Remove(nameof(offer.MinimumShippingCost));
            ModelState.Remove(nameof(offer.CubicMeterCost));
        }
        else if (offer.OfferType == "Cubic")
        {
            ModelState.Remove(nameof(offer.MinimumWeight));
            ModelState.Remove(nameof(offer.MaximumWeight));
            ModelState.Remove(nameof(offer.BaseCost));
            ModelState.Remove(nameof(offer.ExtraCostPerKg));
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model state invalid when creating a new offer: {@Offer}", offer);

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogError("Model state error: {ErrorMessage}", error.ErrorMessage);
                }
            }

            ViewData["Carriers"] = new SelectList(_context.Carriers, "Id", "Name", offer.CarrierId);
            return View(offer);
        }

        _context.Add(offer);
        await _context.SaveChangesAsync();

        foreach (var postalCodeId in postalCodeIds)
        {
            var postalCodeOffer = new PostalCodeOffer
            {
                OfferId = offer.Id,
                PostalCodeId = postalCodeId
            };
            _context.PostalCodeOffers.Add(postalCodeOffer);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Successfully created a new offer with ID: {OfferId}", offer.Id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var offer = await _context.Offers.Include(o => o.PostalCodeOffers).FirstOrDefaultAsync(o => o.Id == id);
        if (offer == null)
        {
            return NotFound();
        }
        ViewData["Carriers"] = new SelectList(_context.Carriers, "Id", "Name", offer.CarrierId);
        ViewData["SelectedPostalCodes"] = offer.PostalCodeOffers?.Select(pc => pc.PostalCodeId).ToList() ?? new List<int>();
        return View(offer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Offer offer, List<int> selectedPostalCodes)
    {
        if (id != offer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(offer);
                await _context.SaveChangesAsync();

                var existingPostalCodes = _context.PostalCodeOffers.Where(pco => pco.OfferId == id).ToList();
                _context.PostalCodeOffers.RemoveRange(existingPostalCodes);

                foreach (var postalCodeId in selectedPostalCodes)
                {
                    var postalCodeOffer = new PostalCodeOffer
                    {
                        OfferId = offer.Id,
                        PostalCodeId = postalCodeId
                    };
                    _context.PostalCodeOffers.Add(postalCodeOffer);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(offer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["Carriers"] = new SelectList(_context.Carriers, "Id", "Name", offer.CarrierId);
        return View(offer);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var offer = await _context.Offers
            .Include(o => o.PostalCodeOffers)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (offer == null)
        {
            return NotFound();
        }

        return View(offer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var offer = await _context.Offers.Include(o => o.PostalCodeOffers).FirstOrDefaultAsync(o => o.Id == id);
        if (offer != null)
        {
            if (offer.PostalCodeOffers != null)
            {
                var postalCodeOffers = _context.PostalCodeOffers.Where(pco => pco.OfferId == id).ToList();
                if (postalCodeOffers != null && postalCodeOffers.Any())
                {
                    _context.PostalCodeOffers.RemoveRange(postalCodeOffers);
                }
            }
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool OfferExists(int id)
    {
        return _context.Offers.Any(e => e.Id == id);
    }

    public async Task<IActionResult> SearchPostalCodes(string term)
    {
        var postalCodes = await _context.PostalCodes
            .Where(pc => (pc.Postal != null && pc.Postal.Contains(term)) ||
                         (pc.Location != null && pc.Location.Contains(term)) ||
                         (pc.Nomos != null && pc.Nomos.Contains(term)))
            .ToListAsync();
        return Json(postalCodes);
    }
}
