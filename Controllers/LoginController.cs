using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IPAddressManagement.Data;
using IPAddressManagement.Models;

namespace IPAddressManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;

        public LoginController(AppDbContext context, ILogger<LoginController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!ValidateInput(username, password))
            {
                _logger.LogWarning($"Invalid input format for username: {username} from IP: {ipAddress}");
                ViewBag.ErrorMessage = "Invalid username or password format.";
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                _logger.LogInformation($"Successful login for username: {username} from IP: {ipAddress} at {DateTime.UtcNow}");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Persistent cookie (survives browser restarts)
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Cookie expiration time
                };

                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogWarning($"Failed login attempt for username: {username} from IP: {ipAddress} at {DateTime.UtcNow}");
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult ForgotPassword()
        {
            var forgotPasswordMessage = _configuration["AppSettings:ForgotPasswordMessage"];
            return Json(new { message = forgotPasswordMessage });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }

        private bool ValidateInput(string username, string password)
        {
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]{3,20}$"))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            return true;
        }
    }
}