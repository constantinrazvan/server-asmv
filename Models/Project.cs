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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Project() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
