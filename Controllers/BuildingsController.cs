using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Requires authentication
public class BuildingsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Buildings";
        return View();
    }
}
