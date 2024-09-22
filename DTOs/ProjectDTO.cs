using System.ComponentModel.DataAnnotations;

namespace ServerAsmv.DTOs
{
    public class ProjectDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Summary { get; set; }
    }
}