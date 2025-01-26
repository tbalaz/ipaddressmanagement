using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Requires authentication
public class LocationsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Locations";
        return View();
    }
}
