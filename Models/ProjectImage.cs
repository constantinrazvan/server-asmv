namespace ServerAsmv.Models
{
    public class ProjectImage
    {
        public long Id { get; set; }
        public string Url { get; set; }
        
        // Foreign key to Project
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
