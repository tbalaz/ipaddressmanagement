using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Models;
using IPAddressManagement.Data;

[Authorize]
public class AddDeviceController : Controller
{
    private readonly AppDbContext _context;

    public AddDeviceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Navigation"] = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Add Device", "/AddDevice")
        };
        return View(new Device());
    }

    [HttpPost]
 
    public IActionResult Index(Device device)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Set timestamps
                device.CreatedAt = DateTime.Now;
                device.UpdatedAt = DateTime.Now;

                // Add to database
                _context.Devices.Add(device);
                _context.SaveChanges();

                // Redirect to Devices page
                return RedirectToAction("Index", "Devices");
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logging framework like Serilog or NLog)
                ModelState.AddModelError("", "An error occurred while saving the device. Please try again.");
            }
        }

        // If validation fails or an error occurs, return to the form with errors
        return View(device);
    }
}