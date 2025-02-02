using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Models;
using IPAddressManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class AddDeviceController : Controller
{
    private readonly AppDbContext _context;

    public AddDeviceController(AppDbContext context)
    {
        _context = context;
    }

    /*
    private readonly ILogger<AddDeviceController> _logger;

    public AddDeviceController(ILogger<AddDeviceController> logger)
    {
        _logger = logger;
    }
    */

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Navigation"] = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Add Device", "/AddDevice")
        };

        // 1) Load existing buildings for the dropdown
        var buildings = _context.Buildings
            .OrderBy(b => b.Name) // optional sort
            .ToList();

        // 2) Pass them to the view in a SelectList
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        // 3) Return a fresh Device model
        return View(new Device());
    }

    [HttpPost]
    public IActionResult Index(Device device)
    {
        // We must reload the buildings dropdown if the form has errors
        var buildings = _context.Buildings.OrderBy(b => b.Name).ToList();
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        // Set audit fields
            device.CreatedAt = DateTime.Now;
            device.UpdatedAt = DateTime.Now;
            device.CreatedBy = User.Identity?.Name;
            device.UpdatedBy = User.Identity?.Name;

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            // Return to the form with validation errors
            //_logger.LogInformation("User logged out. Redirecting to login page.");
            return View(device);
        }

        try
        {

            // Add to database
            _context.Devices.Add(device);
            _context.SaveChanges();

            // Redirect to Devices page
            return RedirectToAction("Index", "Devices");
        }
        catch (Exception ex)
        {
            // Optionally log the error (ex) or display a message
            ModelState.AddModelError("", "An error occurred while saving the device. Please try again.");
            return View(device);
        }
    }
}
