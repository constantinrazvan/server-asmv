namespace ServerAsmv.DTOs
{
    public class ProjectDTO
    {
        public required string Title { get; set; }
        public required string Summary { get; set; }
        public required string Content { get; set; }
        public string? Image { get; set; }
    }
}