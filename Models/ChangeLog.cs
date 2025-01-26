using System;

namespace IPAddressManagement.Models
{
    public enum ChangeType
    {
        Create,
        Update,
        Delete
    }

    public class ChangeLog
    {
        public int LogID { get; set; }
        public int DeviceID { get; set; }
        public ChangeType ChangeType { get; set; }
        public string ChangeDescription { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }

        // Navigation property
        public Device Device { get; set; }
    }
}