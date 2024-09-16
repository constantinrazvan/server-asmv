using AsmvBackend.Models;
using Microsoft.AspNetCore.Http;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerAsmv.Services
{
    public class ProjectsService : IProjects
    {
        private readonly AppData _context;
        private readonly string _imagePath = "uploaded_images";

        public ProjectsService(AppData context)
        {
            _context = context;
            Directory.CreateDirectory(_imagePath); // Ensure the directory exists
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
            if (project.Image != null && project.Image.Length > 0)
            {
                string extension = Path.GetExtension(project.Image.FileName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                destinationPath = Path.Combine(_imagePath, uniqueName);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    project.Image.CopyTo(stream);
                }
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

            if (project.Image != null && project.Image.Length > 0)
            {
                // Delete old image if exists
                if (File.Exists(found.Image))
                {
                    File.Delete(found.Image);
                }

                // Handle new image
                string extension = Path.GetExtension(project.Image.FileName);
                string uniqueName = Guid.NewGuid().ToString() + extension;
                string destinationPath = Path.Combine(_imagePath, uniqueName);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    project.Image.CopyTo(stream);
                }

                found.Image = destinationPath;
            }

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

        public int Count()
        {
            return _context.Projects.Count();
        }
    }
}
