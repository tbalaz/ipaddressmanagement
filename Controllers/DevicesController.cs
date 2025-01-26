using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Data;
using IPAddressManagement.Models;

[Authorize]
public class DevicesController : Controller
{
    private readonly AppDbContext _context;

    public DevicesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder = "hostname_asc", 
                              int page = 1, 
                              int pageSize = 20, 
                              string searchString = "",
                              string statusFilter = "")
    {
        ViewBag.HostnameSort = sortOrder == "hostname_asc" ? "hostname_desc" : "hostname_asc";
        ViewBag.IPSort = sortOrder == "ip_asc" ? "ip_desc" : "ip_asc";
        ViewBag.StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
        
        var query = _context.Devices
            .Include(d => d.ChangeLogs)
            .AsQueryable();

        // Apply status filter
        if (!string.IsNullOrEmpty(statusFilter))
        {
            var filterStatus = Enum.Parse<DeviceStatus>(statusFilter, true);
            query = query.Where(d => d.Status == filterStatus);
        }

        // Apply search filter
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(d => 
                d.Hostname.Contains(searchString) ||
                d.IPAddress.Contains(searchString) ||
                d.Department.Contains(searchString) ||
                d.City.Contains(searchString)
            );
        }

        // Apply sorting
        switch(sortOrder)
        {
            case "hostname_desc":
                query = query.OrderByDescending(d => d.Hostname);
                break;
            case "ip_asc":
                query = query.OrderBy(d => d.IPAddress);
                break;
            case "ip_desc":
                query = query.OrderByDescending(d => d.IPAddress);
                break;
            case "status_asc":
                query = query.OrderBy(d => d.Status);
                break;
            case "status_desc":
                query = query.OrderByDescending(d => d.Status);
                break;
            default:
                query = query.OrderBy(d => d.Hostname);
                break;
        }

        // Pagination
        var totalItems = query.Count();
        var devices = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ViewBag.TotalDevices = _context.Devices.Count(); // Add this line
        ViewBag.ActiveCount = _context.Devices.Count(d => d.Status == DeviceStatus.Active);
        ViewBag.InactiveCount = _context.Devices.Count(d => d.Status == DeviceStatus.Inactive);
        ViewBag.MaintenanceCount = _context.Devices.Count(d => d.Status == DeviceStatus.Maintenance);
        ViewBag.SearchString = searchString;
        ViewBag.StatusFilter = statusFilter;
        
        return View(devices);
    }
}