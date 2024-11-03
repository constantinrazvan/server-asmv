namespace ServerAsmv.Models {
    public class VolunteerImage { 
        public long Id { get; set; }
        public string Url { get; set; }
        
        // Foreign key to Project
        public long VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}