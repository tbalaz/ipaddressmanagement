using System.ComponentModel.DataAnnotations;
namespace IPAddressManagement.Models
{
    public class Building
    {
        [Required(ErrorMessage = "Building Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Number of Floors is required.")]
        [Range(-100, 100, ErrorMessage = "Number of Floors must be between -100 and 100.")]
        public int NumberOfFloors { get; set; }

        [Required(ErrorMessage = "Number of Rooms is required.")]
        [Range(1, 1000, ErrorMessage = "Number of Rooms must be between 1 and 1000.")]
        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Short Name is required.")]
        [StringLength(2, ErrorMessage = "Short Name cannot exceed 2 characters.")]
        public string ShortName { get; set; }
    }
}