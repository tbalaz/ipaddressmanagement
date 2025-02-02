using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System;
using System.Linq;

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
    public IActionResult Index(string searchString, string sortOrder, int page = 1)
    {
        // Retrieve buildings from the database
        var query = _context.Buildings.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(b => b.Name.Contains(searchString) || b.FullAddress.Contains(searchString));
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

    // Additional actions (Edit, Delete) can be added here if needed.
}
