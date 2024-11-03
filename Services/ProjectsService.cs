using ServerAsmv.Models;
using Microsoft.AspNetCore.Http;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServerAsmv.Services
{
    public class ProjectsService : IProjects
    {
        private readonly AppData _context;
        private readonly string _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "projects");

        public ProjectsService(AppData context)
        {
            _context = context;

            // Ensure the images directory exists
            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        public async Task<bool> AddProject(ProjectDTO project, IFormFile photo)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (string.IsNullOrEmpty(project.Title) || string.IsNullOrEmpty(project.Content))
            {
                throw new ArgumentException("Title and Content cannot be null or empty.");
            }

            try
            {
                var found = _context.Projects.FirstOrDefault(p => p.Title == project.Title);
                if (found != null)
                {
                    throw new Exception("Project already exists!");
                }

                string? imagePath = null;
                if (photo != null && photo.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(photo.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        throw new ArgumentException("Invalid image type. Allowed types are: jpg, jpeg, png.");
                    }

                    // Generate a unique file name for the image
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(_imageDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    imagePath = $"/images/projects/{fileName}"; // Save the relative path for database storage
                }

                var newProject = new Project
                {
                    Title = project.Title!,
                    Content = project.Content!,
                    Summary = project.Summary!,
                    ProjectImage = new ProjectImage { Url = imagePath }
                };

                _context.Add(newProject);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding project: " + ex.Message, ex);
            }
        }

        public async Task<bool> UpdateProject(long id, ProjectDTO project, IFormFile photo)
        {
            var found = await _context.Projects.Include(p => p.ProjectImage).FirstOrDefaultAsync(p => p.Id == id);
            if (found == null) throw new KeyNotFoundException("Project not found!");

            if (!string.IsNullOrEmpty(project.Title))
            {
                found.Title = project.Title;
            }

            if (!string.IsNullOrEmpty(project.Content))
            {
                found.Content = project.Content;
            }

            if (!string.IsNullOrEmpty(project.Summary))
            {
                found.Summary = project.Summary;
            }

            if (photo != null && photo.Length > 0)
            {
                // Delete the existing image file, if present
                if (found.ProjectImage != null && !string.IsNullOrEmpty(found.ProjectImage.Url))
                {
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", found.ProjectImage.Url.TrimStart('/'));
                    if (File.Exists(existingFilePath))
                    {
                        File.Delete(existingFilePath);
                    }
                }

                // Save the new image
                var extension = Path.GetExtension(photo.FileName).ToLower();
                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(_imageDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                found.ProjectImage = new ProjectImage { Url = $"/images/projects/{fileName}", ProjectId = found.Id };
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProject(long id)
        {
            var found = await _context.Projects.Include(p => p.ProjectImage).FirstOrDefaultAsync(p => p.Id == id);
            if (found == null) return false;

            // Delete the associated image from the file system
            if (found.ProjectImage != null && !string.IsNullOrEmpty(found.ProjectImage.Url))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", found.ProjectImage.Url.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            _context.Remove(found);
            await _context.SaveChangesAsync();

            return true;
        }

        public Project? GetProject(long id)
        {
            return _context.Projects
                .Include(p => p.ProjectImage)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Project> GetProjects()
        {
            return _context.Projects.Include(p => p.ProjectImage).ToList();
        }

        public int Count()
        {
            return _context.Projects.Count();
        }
    }
}
