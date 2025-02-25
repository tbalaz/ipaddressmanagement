using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System.Collections.Generic;
using System;
using System.Linq;

[Authorize]
public class AddBuildingsController : Controller
{
    private readonly AppDbContext _context;

    public AddBuildingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Navigation"] = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Buildings", "/Buildings")
        };

        // Optionally, if you want to display a view model that includes a list of buildings:
        // var viewModel = new BuildingViewModel
        // {
        //     NewBuilding = new Building(),
        //     Buildings = _context.Buildings.ToList()
        // };
        // return View(viewModel);

        return View(new Building());
    }

    [HttpPost]
    public IActionResult Create(Building building)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(building);
        }

        var now = DateTime.UtcNow;
        var user = User.Identity?.Name;

        building.CreatedAt = now;
        building.UpdatedAt = now;
        building.CreatedBy = user;
        building.UpdatedBy = user;

        _context.Buildings.Add(building);
        _context.SaveChanges();

        // Write an AuditLog entry
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

        // Redirect to the Buildings listing page
        return RedirectToAction("Index", "Buildings");
    }

    // Edit and Delete actions remain unchanged...
}
