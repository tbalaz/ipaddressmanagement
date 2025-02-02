using System;
using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Models
{
    public class Network : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string IpAddress { get; set; } // e.g. "192.168.10.0"

        [Required]
        [StringLength(45)]
        public string Subnet { get; set; } // e.g. "255.255.255.0"

        [Required]
        public int Vlan { get; set; } // VLAN number

        // Foreign key to Building
        public int BuildingId { get; set; }

        // Navigation
        public Building Building { get; set; }
    }
}
