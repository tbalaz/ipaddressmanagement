using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Models;
using IPAddressManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;

[Authorize]
public class AddDeviceController : Controller
{
    private readonly AppDbContext _context;

    public AddDeviceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create(int? buildingId)
    {
        ViewData["Navigation"] = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Add Device", "/AddDevice")
        };

        var buildings = _context.Buildings.OrderBy(b => b.Name).ToList();
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        var device = new Device();
        if (buildingId.HasValue)
        {
            var building = _context.Buildings.Find(buildingId.Value);
            if (building != null)
            {
                device.BuildingId = building.Id;
                device.Building = building;
            }
        }

        return View(device);
    }

    [HttpPost]
    public IActionResult Create(Device device)
    {
        var buildings = _context.Buildings.OrderBy(b => b.Name).ToList();
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        device.CreatedAt = DateTime.Now;
        device.UpdatedAt = DateTime.Now;
        device.CreatedBy = User.Identity?.Name;
        device.UpdatedBy = User.Identity?.Name;

        if (!ModelState.IsValid)
        {
            return View(device);
        }

        try
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            return RedirectToAction("Index", "Devices");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while saving the device. Please try again.");
            return View(device);
        }
    }
}