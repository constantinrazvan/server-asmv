namespace ServerAsmv.Models
{
    public class VolunteerImage
    {
        public long Id { get; set; }
        public string Url { get; set; } // URL to the uploaded image

        public long VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
