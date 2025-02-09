using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IPAddressManagement.Models
{
    public class Building : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Building Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LowestFloor > HighestFloor)
            {
                yield return new ValidationResult(
                    "Lowest floor cannot be greater than the highest floor.",
                    new[] { nameof(LowestFloor), nameof(HighestFloor) });
            }
        }

        [Required(ErrorMessage = "City Name is required.")]
        [StringLength(200, ErrorMessage = "City Name cannot exceed 200 characters.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Street Name is required.")]
        [StringLength(200, ErrorMessage = "Street Name cannot exceed 200 characters.")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Street Number is required.")]
        // This regex requires one or more digits at the beginning,
        // followed by zero, one, or two letters.
        [RegularExpression(@"^[0-9]+[A-Za-z]{0,2}$", 
            ErrorMessage = "Street Number must start with numbers and may be followed by up to 2 letters.")]
        public string StreetNumber { get; set; }

        // Optional: a computed property for convenience
        public string FullAddress => $"{StreetName} {StreetNumber}";

        // Instead of a single NumberOfFloors, use two properties:
        [Required(ErrorMessage = "Lowest floor is required.")]
        [Range(-100, 0, ErrorMessage = "Lowest floor must be between -100 and 0 (if below ground) or 0 if there are no basements.")]
        public int LowestFloor { get; set; }

        [Required(ErrorMessage = "Highest floor is required.")]
        [Range(0, 100, ErrorMessage = "Highest floor must be between 0 and 100.")]
        public int HighestFloor { get; set; }

        public string FloorRange => $"{LowestFloor} to {HighestFloor}";

        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Short Name is required.")]
        [StringLength(2, ErrorMessage = "Short Name cannot exceed 2 characters.")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Branch or HQ is required.")]
        public string OrganizationalUnit { get; set; }

        [BindNever]
        [ValidateNever]
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
