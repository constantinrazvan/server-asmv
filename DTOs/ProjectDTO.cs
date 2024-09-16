namespace ServerAsmv.DTOs
{
    public class ProjectDTO
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
