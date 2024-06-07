using Microsoft.AspNetCore.Mvc;
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
            return View();
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
    }
}
