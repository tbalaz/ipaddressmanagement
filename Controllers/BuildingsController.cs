using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPAddressManagement.Models;
using IPAddressManagement.Data;

[Authorize]
public class BuildingsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Navigation"] = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Buildings", "/Buildings")
        };
        return View();
    }
    [HttpPost]
    public IActionResult Create(Building building)
    {
        if (ModelState.IsValid)
        {
            // Save the building data or perform other operations
            return View(building); // Redirect to a list page or confirmation
        }
        return RedirectToAction("Index");
    }
}
