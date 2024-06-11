using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Added for DbUpdateConcurrencyException
using ShippingCalculator.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace ShippingCalculator.Controllers
{
    public class PostalCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostalCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.PostalCodes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostalCode postalCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postalCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postalCode);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalCode = _context.PostalCodes.Find(id);
            if (postalCode == null)
            {
                return NotFound();
            }
            return View(postalCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostalCode postalCode)
        {
            if (id != postalCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postalCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostalCodeExists(postalCode.Id))
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
            return View(postalCode);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalCode = _context.PostalCodes.Find(id);
            if (postalCode == null)
            {
                return NotFound();
            }

            return View(postalCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postalCode = await _context.PostalCodes.FindAsync(id);
            if (postalCode != null)
            {
                _context.PostalCodes.Remove(postalCode);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PostalCodeExists(int id)
        {
            return _context.PostalCodes.Any(e => e.Id == id);
        }
    }
}
