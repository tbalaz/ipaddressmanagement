using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication services
builder.Services.AddAuthentication("CookieAuth")
    //{
        //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Default scheme
    //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Default challenge scheme
    //}
    //)
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Index"; // Set the login path
        options.AccessDeniedPath = "/Login/Index"; // Set the access denied path
        options.LogoutPath = "/Home/Logout"; // Explicitly set logout path
        options.Cookie.Name = "YourAppCookie"; // Optional: Set a custom cookie name
        options.Cookie.HttpOnly = true; // Ensure the cookie is HTTP-only
        options.Cookie.SameSite = SameSiteMode.Unspecified; // Adjust based on your requirements
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Use "Always" in production
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration time
        options.SlidingExpiration = true; // Enable sliding expiration
    });

builder.Services.AddSession();

var app = builder.Build();

// Migrate and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Apply migrations
    dbContext.Database.Migrate();

    // Seed the admin user if it doesn't exist
    if (!dbContext.Users.Any(u => u.Username == "admin"))
    {
        dbContext.Users.Add(new IPAddressManagement.Models.User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") // Hash the password
        });
        
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

// Use authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();