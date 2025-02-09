using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class BuildingsController : Controller
{
    private readonly AppDbContext _context;

    public BuildingsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Buildings
    [HttpGet]
    public IActionResult Index(string searchString, string sortOrder, int page = 1, string city = null, string street = null, string shortName = null, string organizationalUnit = null)
    {
        // Retrieve buildings from the database
        var query = _context.Buildings.AsQueryable();

         if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(b => b.CityName.ToLower().Contains(city.ToLower()));
        }

        if (!string.IsNullOrEmpty(street))
        {
            query = query.Where(b => b.StreetName.ToLower().Contains(street.ToLower()));
        }

        if (!string.IsNullOrEmpty(shortName))
        {
            query = query.Where(b => b.ShortName.ToLower().Contains(shortName.ToLower()));
        }

        if (!string.IsNullOrEmpty(organizationalUnit))
        {
            query = query.Where(b => b.OrganizationalUnit.ToLower().Contains(organizationalUnit.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(b => b.Name.ToLower().Contains(searchString.ToLower()) || b.FullAddress.ToLower().Contains(searchString.ToLower()));
        }

        // Default sort by Name; you can add additional sorting if needed.
        query = query.OrderBy(b => b.Name);

        // Pagination parameters
        int pageSize = 10;
        int totalBuildings = query.Count();
        ViewBag.TotalBuildings = totalBuildings;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalBuildings / (double)pageSize);
        ViewBag.SearchString = searchString;
        ViewBag.CurrentSort = sortOrder;
        ViewBag.SelectedCity = city;
        ViewBag.SelectedStreet = street;
        ViewBag.SelectedShortName = shortName;
        ViewBag.SelectedOrganizationalUnit = organizationalUnit;

        var buildings = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return View(buildings);
    }

    // GET: /Buildings/Create
    [HttpGet]
    public IActionResult Create()
    {
        // Optionally, you can add navigation breadcrumbs here.
        ViewData["Navigation"] = new[]
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Buildings", "/Buildings"),
            new BreadcrumbItem("Add New Building", "/AddBuildings/Create")
        };

        // Return an empty Building model for the form.
        return View(new Building());
    }

    // POST: /Buildings/Create
    [HttpPost]
    public IActionResult Create(Building building)
    {
        if (!ModelState.IsValid)
        {
            // If validation fails, return the form view with the entered data.
            return View(building);
        }

        // Set auditing fields
        var now = DateTime.UtcNow;
        var user = User.Identity?.Name ?? "System";

        building.CreatedAt = now;
        building.CreatedBy = user;
        building.UpdatedAt = now;
        building.UpdatedBy = user;

        _context.Buildings.Add(building);
        _context.SaveChanges();

        // Optionally, add an AuditLog entry.
        var audit = new AuditLog
        {
            EntityType = "Building",
            EntityId = building.Id,
            Action = AuditAction.Created,
            Description = "Initial building creation",
            ChangedBy = user,
            ChangedAt = now
        };
        _context.AuditLogs.Add(audit);
        _context.SaveChanges();

        // After creation, redirect back to the list of buildings.
        return RedirectToAction("Index");
    }

    // GET: /Buildings/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var building = await _context.Buildings.FindAsync(id);
        if (building == null)
        {
            return NotFound();
        }
        return View(building);
    }

    // POST: /Buildings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityName,StreetName,StreetNumber,LowestFloor,HighestFloor,NumberOfRooms,ShortName,OrganizationalUnit")] Building building)
    {
        if (id != building.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingBuilding = await _context.Buildings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                if (existingBuilding == null)
                {
                    return NotFound();
                }

                building.CreatedBy = existingBuilding.CreatedBy;
                building.CreatedAt = existingBuilding.CreatedAt;
                building.UpdatedAt = DateTime.Now;
                building.UpdatedBy = User.Identity?.Name;
                _context.Update(building);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(building.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Edit));
        }
        return View(building);
    }

    private bool BuildingExists(int id)
    {
        return _context.Buildings.Any(e => e.Id == id);
    }
}