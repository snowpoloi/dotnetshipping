using Microsoft.AspNetCore.Mvc;
using ShippingCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingCalculator.Controllers
{
    public class OffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var offers = _context.Offers.ToList();
            return View(offers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }
    }
}
