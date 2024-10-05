namespace ServerAsmv.DTOs
{
    public class VolunteerDTO
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string City { get; set; }
        public required string Status { get; set; }
        public required string JoinedDate { get; set; }
        public string? ImageUrl { get; set; }

    }
}
