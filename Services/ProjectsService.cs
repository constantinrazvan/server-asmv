using AsmvBackend.Models;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using System.IO;

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
            throw new NotImplementedException();
        }

        public Project GetProject(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjects()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProject(long Id, ProjectDTO project)
        {
            throw new NotImplementedException();
        }
    }
}