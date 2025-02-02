using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Models
{
    public class User : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}