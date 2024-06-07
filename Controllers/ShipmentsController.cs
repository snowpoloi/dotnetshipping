using Microsoft.AspNetCore.Mvc;
using ShippingCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingCalculator.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var shipments = _context.Shipments.ToList();
            return View(shipments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.VolumetricWeight = (shipment.Length * shipment.Width * shipment.Height) / 5000;
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }
    }
}
