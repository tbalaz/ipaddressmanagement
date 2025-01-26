using System;
using System.Collections.Generic;
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

    public class Device
    {
        public int DeviceID { get; set; }

        [Required(ErrorMessage = "IP Address is required")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Invalid IP Address")]
        public string IPAddress { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public DeviceStatus Status { get; set; }

        [Required(ErrorMessage = "Hostname is required.")]
        [RegularExpression(@"^[A-Za-z-]+$", ErrorMessage = "Hostname cannot contain numbers.")]
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

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(100, ErrorMessage = "Street cannot exceed 100 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Building is required")]
        [StringLength(100, ErrorMessage = "Building cannot exceed 100 characters")]
        public string Building { get; set; }

        [Required(ErrorMessage = "Floor is required")]
        [Range(0, 100, ErrorMessage = "Floor must be between 0 and 100")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Room is required")]
        [StringLength(50, ErrorMessage = "Room cannot exceed 50 characters")]
        public string Room { get; set; }

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "UpdatedAt is required")]
        public DateTime UpdatedAt { get; set; }

        // Navigation property for ChangeLogs
        public List<ChangeLog> ChangeLogs { get; set; } = new List<ChangeLog>();
    }
}