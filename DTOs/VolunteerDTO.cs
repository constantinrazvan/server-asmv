using System.ComponentModel.DataAnnotations;

namespace ServerAsmv.DTOs {
    public class VolunteerDTO { 
        public required string Firstname {get; set;}
        public required string Lastname {get; set;} 
        public required string Email {get; set;} 
        public string? JoinedDate {get; set;} 
        public bool President {get; set;}
        public bool VicePresident {get; set;}
        public required string Department { get; set; }
        public string? ImageUrl { get; set; }
        public string phoneNumber { get; set; }
    }
}