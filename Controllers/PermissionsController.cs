using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Requires authentication
public class PermissionsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Permissions";
        return View();
    }
}
