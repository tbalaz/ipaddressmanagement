using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Requires authentication
public class AddUserController : Controller
{
    public IActionResult Index()
{
    ViewData["Navigation"] = new List<BreadcrumbItem>
    {
        new BreadcrumbItem("Home", "/"),
        new BreadcrumbItem("Networks", "/Networks")
    };
    return View();
}

// Breadcrumb model
public class BreadcrumbItem
{
    public string Title { get; set; }
    public string Url { get; set; }
    
    public BreadcrumbItem(string title, string url)
    {
        Title = title;
        Url = url;
    }
}
}
