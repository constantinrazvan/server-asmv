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

namespace ServerAsmv.Services
{
    public class ProjectsService : IProjects
    {
        private readonly AppData _context;
        private readonly string _imageFolderPath;

        public ProjectsService(AppData context)
        {
            _context = context;
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploaded_images");
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        public async Task<bool> AddProject(ProjectDTO project)
        {
            var found = _context.Projects.FirstOrDefault(p => p.Title == project.Title);
            if (found != null)
            {
                throw new Exception("Project already exists!");
            }

            string? imagePath = null;
            if (project.Image != null && project.Image.Length > 0)
            {
                imagePath = await SaveImage(project.Image);
            }

            var newProject = new Project
            {
                Title = project.Title!,
                Content = project.Content!,
                Summary = project.Summary!,
                Image = imagePath ?? string.Empty
            };

            _context.Add(newProject);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateProject(long id, ProjectDTO project)
        {
            var found = await _context.Projects.FindAsync(id);
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

            if (project.Image != null && project.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(found.Image))
                {
                    DeleteImage(found.Image);
                }

                var newImagePath = await SaveImage(project.Image);
                found.Image = newImagePath;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProject(long id)
        {
            var found = await _context.Projects.FindAsync(id);
            if (found == null) return false;

            if (!string.IsNullOrEmpty(found.Image))
            {
                DeleteImage(found.Image);
            }

            _context.Remove(found);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(_imageFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Path.Combine("uploaded_images", fileName); // Path relative to the server root
        }

        private void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(_imageFolderPath, Path.GetFileName(imagePath));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
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

        public async Task<byte[]> GetProjectImage(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null || string.IsNullOrEmpty(project.Image))
            {
                throw new FileNotFoundException("Project or image not found.");
            }

            var imagePath = Path.Combine("UploadedImages", Path.GetFileName(project.Image)); // Adjust path if needed

            if (!System.IO.File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found.");
            }

            return await System.IO.File.ReadAllBytesAsync(imagePath);
        }
    }
}