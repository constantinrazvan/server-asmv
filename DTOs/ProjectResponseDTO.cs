namespace ServerAsmv.DTOs {
    public class ProjectResponseDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string? ImageUrl { get; set; }
    }
}