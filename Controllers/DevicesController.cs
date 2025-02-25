using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Data;
using IPAddressManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                              string statusFilter = "",
                              int? buildingId = null)
    {
        ViewBag.HostnameSort = sortOrder == "hostname_asc" ? "hostname_desc" : "hostname_asc";
        ViewBag.IPSort = sortOrder == "ip_asc" ? "ip_desc" : "ip_asc";
        ViewBag.StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
        
        var querydevice = _context.Devices
            .Include(d => d.ChangeLogs)
            .Include(d => d.Building) 
            .AsQueryable();

        // Filter by buildingId
        if (buildingId.HasValue)
        {
            querydevice = querydevice.Where(d => d.BuildingId == buildingId.Value);
        }

        // Apply status filter
        if (!string.IsNullOrEmpty(statusFilter))
        {
            var filterStatus = Enum.Parse<DeviceStatus>(statusFilter, true);
            querydevice = querydevice.Where(d => d.Status == filterStatus);
        }

        // Apply search filter
        if (!string.IsNullOrEmpty(searchString))
        {
            querydevice = querydevice.Where(d => 
                d.Hostname.Contains(searchString) ||
                d.IPAddress.Contains(searchString) ||
                d.Department.Contains(searchString)
            );
        }

        // Apply sorting
        switch(sortOrder)
        {
            case "hostname_desc":
                querydevice = querydevice.OrderByDescending(d => d.Hostname);
                break;
            case "ip_asc":
                querydevice = querydevice.OrderBy(d => d.IPAddress);
                break;
            case "ip_desc":
                querydevice = querydevice.OrderByDescending(d => d.IPAddress);
                break;
            case "status_asc":
                querydevice = querydevice.OrderBy(d => d.Status);
                break;
            case "status_desc":
                querydevice = querydevice.OrderByDescending(d => d.Status);
                break;
            default:
                querydevice = querydevice.OrderBy(d => d.Hostname);
                break;
        }

        // Pagination
        var totalItems = querydevice.Count();
        var devices = querydevice
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ViewBag.TotalDevices = _context.Devices.Count();
        ViewBag.ActiveCount = _context.Devices.Count(d => d.Status == DeviceStatus.Active);
        ViewBag.InactiveCount = _context.Devices.Count(d => d.Status == DeviceStatus.Inactive);
        ViewBag.MaintenanceCount = _context.Devices.Count(d => d.Status == DeviceStatus.Maintenance);
        ViewBag.SearchString = searchString;
        ViewBag.StatusFilter = statusFilter;
        ViewBag.BuildingId = buildingId;
        
        return View(devices);
    }

    // GET: Devices/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var device = await _context.Devices.FindAsync(id);
        if (device == null)
        {
            return NotFound();
        }

        var buildings = _context.Buildings.OrderBy(b => b.Name).ToList();
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        return View(device);
    }

    // POST: Devices/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("DeviceID,IPAddress,Status,Hostname,Department,EquipmentType,Criticality,MACAddress,BuildingId,Floor,Room,CreatedBy,CreatedAt")] Device device)
    {
        if (id != device.DeviceID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingDevice = await _context.Devices.AsNoTracking().FirstOrDefaultAsync(d => d.DeviceID == id);
                if (existingDevice == null)
                {
                    return NotFound();
                }
                device.UpdatedAt = DateTime.Now;
                device.UpdatedBy = User.Identity?.Name;
                device.CreatedBy = existingDevice.CreatedBy;
                device.CreatedAt = existingDevice.CreatedAt;

                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        var buildings = _context.Buildings.OrderBy(b => b.Name).ToList();
        ViewBag.Buildings = new SelectList(buildings, "Id", "Name");

        return View(device);
    }

    private bool DeviceExists(int id)
    {
        return _context.Devices.Any(e => e.DeviceID == id);
    }
}