using System.ComponentModel.DataAnnotations;

namespace ServerAsmv.DTOs
{
    public class VolunteerDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        public required string Firstname { get; set; }
        
        [Required(ErrorMessage = "Last name is required.")]
        public required string Lastname { get; set; }
        
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Phone number is required.")]
        public required string Phone { get; set; }
        
        [Required(ErrorMessage = "City is required.")]
        public required string City { get; set; }
        
        [Required(ErrorMessage = "Status is required.")]
        public required string Status { get; set; }
        
        public string Ocupation { get; set; }
        
        [Required(ErrorMessage = "Joined date is required.")]
        public required string JoinedDate { get; set; }

        public string? ImageUrl { get; set; } // To store the URL of the uploaded image
    }
}
