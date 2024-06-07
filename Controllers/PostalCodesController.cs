using Microsoft.AspNetCore.Mvc;
using ShippingCalculator.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace ShippingCalculator.Controllers
{
    public class PostalCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostalCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var postalCodes = await _context.PostalCodes.ToListAsync();
            return View(postalCodes);
        }

        [HttpPost]
        public async Task<IActionResult> ImportCsv()
        {
            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PostalCode>().ToList();
                    _context.PostalCodes.AddRange(records);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PostalCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalCode = await _context.PostalCodes.FindAsync(id);
            if (postalCode == null)
            {
                return NotFound();
            }
            return View(postalCode);
        }

        // POST: PostalCodes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Postal,Location,Nomos")] PostalCode postalCode)
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

        // GET: PostalCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalCode = await _context.PostalCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postalCode == null)
            {
                return NotFound();
            }

            return View(postalCode);
        }

        // POST: PostalCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postalCode = await _context.PostalCodes.FindAsync(id);
            _context.PostalCodes.Remove(postalCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostalCodeExists(int id)
        {
            return _context.PostalCodes.Any(e => e.Id == id);
        }
    }
}
