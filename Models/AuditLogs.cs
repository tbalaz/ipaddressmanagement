using System;
using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Models
{
    public enum AuditAction
    {
        Created,
        Updated,
        Deleted
    }

    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EntityType { get; set; }  // e.g. "Building", "Device", "User"

        public int EntityId { get; set; }       // The primary key of that entity

        public AuditAction Action { get; set; } // Created, Updated, or Deleted

        public string Description { get; set; } // e.g. "Building name changed from 'A' to 'B'"

        [Required]
        [StringLength(100)]
        public string ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
