using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using ServerAsmv.CustomQueries;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using ServerAsmv.Models;

namespace ServerAsmv.Services {
    public class VolunteerService : IVolunteer, VolunteerRepository
    {
        private readonly AppData _context;
        private readonly PhotoService _photoService;
        private readonly string _baseUrl;


        public VolunteerService(AppData context, PhotoService photoService, IConfiguration configuration)
        {
            _context = context;
            _photoService = photoService;
            _baseUrl = configuration["BaseUrl"] ?? "http://localhost:5235"; // Default pentru local

        }

        public async Task<string> AddVolunteer(VolunteerDTO newVolunteer, IFormFile? photo)
        {
            // Verificăm dacă există deja un voluntar cu același email
            var found = await _context.Volunteers.FirstOrDefaultAsync(v => v.Email == newVolunteer.Email);
            if (found != null)
            {
                return "Volunteer already exists!";
            }

            // Verificăm dacă există deja un președinte pentru departament
            if (newVolunteer.President)
            {
                var existingPresident = await _context.Volunteers
                    .FirstOrDefaultAsync(v => v.President && v.Department == newVolunteer.Department);
                if (existingPresident != null)
                {
                    return $"Departamentul {newVolunteer.Department} are deja un președinte.";
                }
            }

            // Verificăm dacă există deja un vicepreședinte pentru departament
            if (newVolunteer.VicePresident)
            {
                var existingVicePresident = await _context.Volunteers
                    .FirstOrDefaultAsync(v => v.VicePresident && v.Department == newVolunteer.Department);
                if (existingVicePresident != null)
                {
                    return $"Departamentul {newVolunteer.Department} are deja un vicepreședinte.";
                }
            }

            // Gestionăm încărcarea imaginii dacă există
            string? imagePath = null;
            if (photo != null && photo.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(photo.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new ArgumentException("Invalid image type. Allowed types are: jpg, jpeg, png.");
                }

                var uploadsFolder = Path.Combine("wwwroot", "images", "volunteers");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                imagePath = $"/images/volunteers/{uniqueFileName}";
            }

            // Creăm un nou obiect Volunteer
            var volunteer = new Volunteer
            {
                Firstname = newVolunteer.Firstname,
                Lastname = newVolunteer.Lastname,
                Email = newVolunteer.Email,
                JoinedDate = newVolunteer.JoinedDate,
                Department = newVolunteer.Department,
                President = newVolunteer.President,
                VicePresident = newVolunteer.VicePresident,
                VolunteerImage = imagePath != null ? new VolunteerImage { Url = imagePath } : null, // Setăm doar dacă există o imagine
                PhoneNumber = newVolunteer.PhoneNumber,
                Ocupation = newVolunteer.Ocupation
            };

            try
            {
                // Salvăm voluntarul în baza de date
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return "Volunteer added successfully!";
            }
            catch (Exception ex)
            {
                // Gestionăm eventualele erori și logăm excepțiile
                Console.WriteLine($"Error while saving volunteer: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while adding the volunteer. Please try again.");
            }
        }


        public int Count()
        {
            return _context.Volunteers.Count();
        }

        public Volunteer? GetVolunteer(long id)
        {
            var volunteer = _context.Volunteers
                .Include(v => v.VolunteerImage)
                .FirstOrDefault(v => v.Id == id);

            if (volunteer?.VolunteerImage != null)
            {
                volunteer.VolunteerImage.Url = $"{_baseUrl}{volunteer.VolunteerImage.Url}";
            }

            return volunteer;
        }


        public async Task<List<Volunteer>> GetVolunteers()
        {
            return await _context.Volunteers
                .Include(v => v.VolunteerImage)
                .ToListAsync();
        }

        public async Task<(bool, string message)> RemoveVolunteer(long id)
        {
            Volunteer? found = await _context.Volunteers
                .Include(v => v.VolunteerImage) 
                .FirstOrDefaultAsync(v => v.Id == id);

            if (found == null)
            {
                return (false, "Volunteer not found!");
            }

            if (found.VolunteerImage != null && !string.IsNullOrEmpty(found.VolunteerImage.Url))
            {
                var imagePath = Path.Combine("wwwroot", found.VolunteerImage.Url.TrimStart('/'));

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            _context.Volunteers.Remove(found);
            await _context.SaveChangesAsync();

            return (true, "Volunteer and image have been removed successfully!");
        }

        public async Task<Volunteer?> SelectPresidentAsync(string department)
        {
            return await _context.Volunteers
                .Where(v => v.President && v.Department == department)
                .SingleOrDefaultAsync();
        }

        public async Task<Volunteer?> SelectVicePresidentAsync(string department)
        {
            return await _context.Volunteers
                .Where(v => v.VicePresident && v.Department == department)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateVolunteer(long id, VolunteerDTO volunteer, IFormFile photo)
        {
            Volunteer? found = await _context.Volunteers
                                            .Include(v => v.VolunteerImage)
                                            .FirstOrDefaultAsync(v => v.Id == id);

            if (found == null)
            {
                return false;
            }

            // Actualizare informații voluntar
            if (!string.IsNullOrEmpty(volunteer.Firstname))
            {
                found.Firstname = volunteer.Firstname;
            }

            if (!string.IsNullOrEmpty(volunteer.Lastname))
            {
                found.Lastname = volunteer.Lastname;
            }

            if (!string.IsNullOrEmpty(volunteer.Email))
            {
                found.Email = volunteer.Email;
            }

            if (!string.IsNullOrEmpty(volunteer.JoinedDate))
            {
                found.JoinedDate = volunteer.JoinedDate;
            }

            found.President = volunteer.President;
            found.VicePresident = volunteer.VicePresident;

            if (photo != null && photo.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(photo.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new ArgumentException("Invalid image type. Allowed types are: jpg, jpeg, png.");
                }

                if (found.VolunteerImage != null && !string.IsNullOrEmpty(found.VolunteerImage.Url))
                {
                    var oldImagePath = Path.Combine("wwwroot", found.VolunteerImage.Url.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                var uploadsFolder = Path.Combine("wwwroot", "images", "volunteers");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                if (found.VolunteerImage != null)
                {
                    found.VolunteerImage.Url = $"/images/volunteers/{uniqueFileName}";
                }
                else
                {
                    found.VolunteerImage = new VolunteerImage
                    {
                        Url = $"/images/volunteers/{uniqueFileName}",
                        VolunteerId = found.Id
                    };
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Volunteer?>> SelectByDepartment(string department)
        {
            return await _context.Volunteers
                                   .Include(x => x.Department)
                                   .ToListAsync();
        }
    }
}