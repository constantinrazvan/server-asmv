using AsmvBackend.Models;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using System.IO;

namespace ServerAsmv.Services
{
    public class ProjectsService : IProjects
    {
        private readonly AppData _context;

        public ProjectsService(AppData context)
        {
            _context = context;
        }

        public bool AddProject(ProjectDTO project)
        {
            // Check if the project already exists
            var found = _context.Projects.FirstOrDefault(p => p.Title == project.Title);
            if (found != null)
            {
                throw new Exception("Project already exists!");
            }

            // Handle image
            string? destinationPath = null;
            if (!string.IsNullOrEmpty(project.Image))
            {
                string path = "uploaded_images";
                Directory.CreateDirectory(path);

                string imageName = Path.GetFileName(project.Image);
                string extension = Path.GetExtension(imageName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                destinationPath = Path.Combine(path, uniqueName);

                // Assuming project.Image is a path to the image file on the server
                File.Copy(project.Image, destinationPath);
            }

            // Create and save the new project
            var newProject = new Project
            {
                Title = project.Title,
                Content = project.Content,
                Summary = project.Summary,
                Image = destinationPath ?? string.Empty
            };

            _context.Add(newProject);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteProject(long id)
        {
            var found = _context.Projects.Find(id);
            if (found == null) return false;

            // Delete image file if it exists
            if (File.Exists(found.Image))
            {
                File.Delete(found.Image);
            }

            _context.Remove(found);
            _context.SaveChanges();

            return true;
        }

        public Project? GetProject(long id)
        {
            return _context.Projects.Find(id);
        }


        public List<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public bool UpdateProject(long id, ProjectDTO project)
        {
            var found = _context.Projects.Find(id);
            if (found == null) throw new KeyNotFoundException("Project not found!");

            // Update project fields
            if (!string.IsNullOrEmpty(project.Title) && project.Title != found.Title)
            {
                found.Title = project.Title;
            }

            if (!string.IsNullOrEmpty(project.Content) && project.Content != found.Content)
            {
                found.Content = project.Content;
            }

            if (!string.IsNullOrEmpty(project.Summary) && project.Summary != found.Summary)
            {
                found.Summary = project.Summary;
            }

            if (!string.IsNullOrEmpty(project.Image) && project.Image != found.Image)
            {
                // Delete old image if exists
                if (File.Exists(found.Image))
                {
                    File.Delete(found.Image);
                }

                // Handle new image
                string path = "uploaded_images";
                Directory.CreateDirectory(path);

                string imageName = Path.GetFileName(project.Image);
                string extension = Path.GetExtension(imageName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                string destinationPath = Path.Combine(path, uniqueName);

                File.Copy(project.Image, destinationPath);

                found.Image = destinationPath;
            }

            _context.SaveChanges();

            return true;
        }

        public int Count()
        {
            return _context.Projects.Count();
        }
    }
}