namespace ServerAsmv.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public  string Content { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; } // URL sau calea imaginii

        // Constructor fără parametri
        public Project() { }

        // Constructor cu parametri
        public Project(string title, string content, string summary, string image)
        {
            Title = title;
            Content = content;
            Summary = summary;
            Image = image;
        }

        // Suprascrierea metodei ToString pentru a afișa obiectul
        public override string ToString()
        {
            return $"Project{{ id={Id}, title='{Title}', content='{Content}', summary='{Summary}', image='{Image}' }}";
        }
    }
}
