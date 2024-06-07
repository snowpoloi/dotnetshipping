using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingCalculator.Controllers
{
    public class CarriersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarriersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Carriers.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carrier carrier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrier);
        }
    }
}
