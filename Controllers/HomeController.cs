using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[Authorize] // Ensures only authenticated users can access
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
        {
            Console.WriteLine("Logout action called."); // Check server logs
            await HttpContext.SignOutAsync("CookieAuth");
            //HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
            //_logger.LogInformation("User logged out. Redirecting to login page.");
//return RedirectToAction("Index", "Login");
        }
}