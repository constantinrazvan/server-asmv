namespace ServerAsmv.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }

        // Navigation property for related image
        public ProjectImage ProjectImage { get; set; }

        // Constructor fără parametri
        public Project() { }

        // Constructor cu parametri
        public Project(string title, string content, string summary)
        {
            Title = title;
            Content = content;
            Summary = summary;
        }

        // Suprascrierea metodei ToString pentru a afișa obiectul
        public override string ToString()
        {
            return $"Project{{ id={Id}, title='{Title}', content='{Content}', summary='{Summary}' }}";
        }
    }
}
