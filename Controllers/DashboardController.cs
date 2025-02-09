using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var buildingsCount = await _context.Buildings.CountAsync();
        var devicesCount = await _context.Devices.CountAsync();
        var locationsCount = await _context.Buildings.Select(b => b.CityName).Distinct().CountAsync();

        var model = new DashboardViewModel
        {
            BuildingsCount = buildingsCount,
            DevicesCount = devicesCount,
            LocationsCount = locationsCount
        };

        return View(model);
    }
}