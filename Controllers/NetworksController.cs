using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Requires authentication
public class NetworksController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "AddDevice";
        return View();
    }
}
