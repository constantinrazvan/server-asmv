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
        private readonly PhotoService _photoService;

        public ProjectsService(AppData context, PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
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

                    var uploadResult = await _photoService.AddPhotoAsync(photo);
                    imagePath = uploadResult.Url.ToString();
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
                if (found.ProjectImage != null)
                {
                    await _photoService.DeletePhotoAsync(found.ProjectImage.Url); // Delete the old image
                }

                var uploadResult = await _photoService.AddPhotoAsync(photo);
                found.ProjectImage = new ProjectImage { Url = uploadResult.Url.ToString(), ProjectId = found.Id }; // Update the image reference
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProject(long id)
        {
            var found = await _context.Projects.Include(p => p.ProjectImage).FirstOrDefaultAsync(p => p.Id == id);
            if (found == null) return false;

            // Șterge imaginea de pe Cloudinary (sau alt serviciu de stocare)
            if (found.ProjectImage != null && !string.IsNullOrEmpty(found.ProjectImage.Url))
            {
                var publicId = ExtractPublicId(found.ProjectImage.Url);
                await _photoService.DeletePhotoAsync(publicId);
            }

            // Șterge proiectul din baza de date
            _context.Remove(found);
            await _context.SaveChangesAsync();

            return true;
        }

        private string ExtractPublicId(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var segments = uri.Segments;

            return segments[segments.Length - 1].Split('.')[0]; 
        }

        public Project? GetProject(long id)
        {
            return _context.Projects
                .Include(p => p.ProjectImage) // Include the related ProjectImage entity
                .FirstOrDefault(p => p.Id == id); // Find the project by its Id
        }

        public List<Project> GetProjects()
        {
            var projects = _context.Projects.Include(p => p.ProjectImage).ToList();
            if (projects == null || !projects.Any())
            {
                Console.WriteLine("No projects found in the database.");
            }
            return projects;
        }

        public int Count()
        {
            return _context.Projects.Count();
        }

        public async Task<byte[]> GetProjectImage(long id)
        {
            var project = await _context.Projects.Include(p => p.ProjectImage).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null || project.ProjectImage == null || string.IsNullOrEmpty(project.ProjectImage.Url))
            {
                throw new FileNotFoundException("Project or image not found.");
            }

            var imagePath = Path.Combine("UploadedImages", Path.GetFileName(project.ProjectImage.Url)); // Adjust path if needed

            if (!System.IO.File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found.");
            }

            return await System.IO.File.ReadAllBytesAsync(imagePath);
        }
    }
}
