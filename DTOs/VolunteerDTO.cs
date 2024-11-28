using System.ComponentModel.DataAnnotations;

namespace ServerAsmv.DTOs
{
    public class VolunteerDTO
    {
        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(50, ErrorMessage = "Firstname cannot exceed 50 characters.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(100, ErrorMessage = "Lastname cannot exceed 50 characters.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [MaxLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string Department { get; set; }

        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must have 10 digits.")]
        public string? PhoneNumber { get; set; }

        public string? JoinedDate { get; set; } // Assuming frontend sends this in a valid format (e.g., ISO 8601)

        public bool President { get; set; }
        public bool VicePresident { get; set; }

        public string? ImageUrl { get; set; }
        public string Ocupation { get; set; }
    }
}
