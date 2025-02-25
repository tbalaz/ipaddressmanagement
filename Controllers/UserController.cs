using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System.Threading.Tasks;

[Authorize]
public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Users/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Username,PasswordHash,FirstName,LastName,Department,Email,PhoneNumber,Company,Role")] User user)
    {
        if (ModelState.IsValid)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // Hash the password
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.CreatedBy = User.Identity.Name;
            user.UpdatedBy = User.Identity.Name;

            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Index
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);
    }
}