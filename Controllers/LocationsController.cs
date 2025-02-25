using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Data;
using System.Linq;

namespace IPAddressManagement.Controllers
{
    public class LocationsController : Controller
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Locations
        public IActionResult Index()
        {
            // Fetch distinct cities from the Buildings table
            var cities = _context.Buildings
                .Select(b => b.CityName)
                .Distinct()
                .ToList();

            return View(cities);
        }
    }
}