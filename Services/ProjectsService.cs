using AsmvBackend.Models;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;

namespace ServerAsmv.Services {
    public class ProjectsService : IProjects
    {
        private readonly AppData _context;

        public ProjectsService(AppData context){
            this._context = context;
        }

        public bool AddProject(ProjectDTO project)
        {
            Project? found = _context.Projects.Find(project.Title);
        
            if (found != null)
            {
                throw new Exception("Already exists!");
            }
        
            string? image = project.Image;
            string? destinationPath = null;
        
            if (!string.IsNullOrEmpty(image))
            {
                string path = "uploaded_images";
                bool exists = Directory.Exists(path);
        
                if (!exists)
                {
                    Directory.CreateDirectory(path);
                }
        
                string imageName = Path.GetFileName(image);
                string extension = Path.GetExtension(imageName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                destinationPath = Path.Combine(path, uniqueName);
        
                File.Copy(image, destinationPath);
            }
        
            Project newProject = new Project(
                title: project.Title,
                content: project.Content,
                summary: project.Summary,
                image: destinationPath ??  ""
            );

            _context.Add(newProject);
            _context.SaveChanges();
        
            return true;
        }

        public bool DeleteProject(long Id)
        {
            Project? found = _context.Projects.Find(Id);

            if(found == null) {
                return false;
            }

            if (File.Exists(found.Image)) {
                File.Delete(found.Image);
            }

            _context.Remove(found);

            return true;
        }

        public Project GetProject(long Id)
        {
            Project? found = _context.Projects.Find(Id);

            if(found == null) {
                return null!;
            }

            return found;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            foreach(Project project in _context.Projects) {
                projects.Add(project);
            }

            if(projects.Count == 0) {
                return null!;
            }

            return projects;
        }

        public bool UpdateProject(long Id, ProjectDTO project)
        {
            Project? found = _context.Projects.Find(Id);
        
            if(found == null) {
                throw new KeyNotFoundException("Couldn't be found!");
            }
        
            if(project.Title != null && project.Title != found.Title)
            {
                found.Title = project.Title;
            }
        
            if(project.Content != null && project.Content != found.Content)
            {
                found.Content = project.Content;
            }
        
            if(project.Summary != null && project.Summary != found.Summary)
            {
                found.Summary = project.Summary;
            }
        
            if(project.Image != null && project.Image != found.Image)
            {
                string? destinationPath = null;
        
                if (!string.IsNullOrEmpty(project.Image))
                {
                    string path = "uploaded_images";
                    bool exists = Directory.Exists(path);
        
                    if (!exists)
                    {
                        Directory.CreateDirectory(path);
                    }
        
                    string imageName = Path.GetFileName(project.Image);
                    string extension = Path.GetExtension(imageName);
                    string uniqueName = Guid.NewGuid().ToString() + extension;
                    destinationPath = Path.Combine(path, uniqueName);
        
                    File.Copy(project.Image, destinationPath);
                }
        
                if (File.Exists(found.Image))
                {
                    File.Delete(found.Image);
                }
        
                found.Image = destinationPath ?? "";
            }
        
            _context.SaveChanges();
        
            return true;
        }

        public int Count()
        {
            int number = 0;

            foreach(Project p in _context.Projects) {
                number++;
            }

            return number;
        }
    }
}