using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Models
{
    public enum DeviceStatus
    {
        Active,
        Inactive,
        Maintenance
    }

    public enum CriticalityLevel
    {
        High,
        Medium,
        Low
    }

    // Make sure this inherits from AuditableEntity so we get CreatedAt/UpdatedAt, etc.
    public class Device : AuditableEntity
    {
        [Key]
        public int DeviceID { get; set; }

        [Required(ErrorMessage = "IP Address is required")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Invalid IP Address")]
        public string IPAddress { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public DeviceStatus Status { get; set; }

        [Required(ErrorMessage = "Hostname is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_/-]{3,25}$", ErrorMessage = "Hostname can only contain letters, numbers, hypen and underline.")]
        public string Hostname { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Equipment Type is required")]
        [StringLength(100, ErrorMessage = "Equipment Type cannot exceed 100 characters")]
        public string EquipmentType { get; set; }

        [Required(ErrorMessage = "Criticality is required")]
        public CriticalityLevel Criticality { get; set; }

        [Required(ErrorMessage = "MAC Address is required")]
        [RegularExpression(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", ErrorMessage = "Invalid MAC Address")]
        public string MACAddress { get; set; }

        // Instead of a string "Building", we now have a foreign key and navigation property:
        [Required(ErrorMessage = "Building is required")]
        public int BuildingId { get; set; } // FK to the Building table

        [ValidateNever]
        [BindNever]
        public Building Building { get; set; }

        [Required(ErrorMessage = "Floor is required")]
        [Range(-10, 40, ErrorMessage = "Floor must be between -10 and 40")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Room is required")]
        [StringLength(50, ErrorMessage = "Room cannot exceed 50 characters")]
        public string Room { get; set; }

        // Remove these, since they're inherited from AuditableEntity:
        // [Required(ErrorMessage = "CreatedAt is required")]
        // public DateTime CreatedAt { get; set; }
        //
        // [Required(ErrorMessage = "UpdatedAt is required")]
        // public DateTime UpdatedAt { get; set; }

        // Navigation property for ChangeLogs
        public List<ChangeLog> ChangeLogs { get; set; } = new List<ChangeLog>();
    }
}
