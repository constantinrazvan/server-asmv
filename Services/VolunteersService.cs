using ServerAsmv.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;
using ServerAsmv.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ServerAsmv.Services {
    public class VolunteersService : IVolunteer
    {
        private readonly AppData _context;
        private readonly PhotoService _photoService;

        public VolunteersService(AppData _context, PhotoService _service) {
            this._context = _context;
            this._photoService = _service;
        }

        public async Task AddVolunteer(VolunteerDTO volunteer, IFormFile photo)
        {
            if(volunteer == null) {
                throw new ArgumentNullException(nameof(volunteer));
            }
            if(string.IsNullOrEmpty(volunteer.Firstname) || string.IsNullOrEmpty(volunteer.Lastname) || string.IsNullOrEmpty(volunteer.Email) || string.IsNullOrEmpty(volunteer.Phone) || string.IsNullOrEmpty(volunteer.JoinedDate) || string.IsNullOrEmpty(volunteer.City)) {
                throw new ArgumentException("All fields should be filled!");
            }

            try { 
                var found = _context.Volunteers.FirstOrDefault(v => v.Email == volunteer.Email);
                if(found != null) 
                {
                    throw new Exception("Volunteer already exists!");
                }

                string? imagePath = null; 

                if(photo != null && photo.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
                    var extension = Path.GetExtension(photo.FileName).ToLower();
                    if(!allowedExtensions.Contains(extension))
                    {
                        throw new ArgumentException("Invalid image type. Allowed types are: jpg, jpeg, png.");
                    }

                    var uploadResult = await _photoService.AddPhotoAsync(photo); // Add photo using a photo service
                    imagePath = uploadResult.Url.ToString();
                }

#pragma warning disable CS8601 // Possible null reference assignment.
                var newVolunteer = new Volunteer
                {
                    Firstname = volunteer.Firstname!,
                    Lastname = volunteer.Lastname!,
                    Email = volunteer.Email!,
                    Phone = volunteer.Phone!,
                    City = volunteer.City!,
                    Status = volunteer.Status!,
                    Ocupation = volunteer.Ocupation,
                    JoinedDate = volunteer.JoinedDate!,
                    VolunteerImage = new VolunteerImage { Url = imagePath } // Save the image URL
                };
#pragma warning restore CS8601 // Possible null reference assignment.

                _context.Add(newVolunteer);
                await _context.SaveChangesAsync();
            } 
            catch(Exception ex)
            {
                throw new Exception("Error while adding volunteer: " + ex.Message);
            }
        }

        public void DeleteVolunteer(long Id)
        {
            if(Id <= 0) {
                throw new ArgumentException("Id cannot be <= 0 !");
            }

            Volunteer? found = _context.Volunteers.Find(Id);

            if(found == null) {
                throw new KeyNotFoundException("Volunteer with id doesnt exist!");
            }

            _context.Volunteers.Remove(found);
            _context.SaveChanges();
        }

        public Volunteer? GetVolunteer(long id)
        {
            return _context.Volunteers
                .Include(v => v.VolunteerImage)
                .FirstOrDefault(v => v.Id == id);
        }

        public List<Volunteer> GetVolunteers()
        {
            // Retrieve all volunteers from the context and convert to a List
            List<Volunteer> volunteers = _context.Volunteers.ToList();
            
            return volunteers;
        }

        public void UpdateVolunteer(long id, Volunteer volunteer)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0", nameof(id));
            }

            var found = _context.Volunteers.Find(id);

            if (found == null)
            {
                throw new KeyNotFoundException("Volunteer with this id doesn't exist!");
            }

            if (!string.IsNullOrEmpty(volunteer.Firstname) && found.Firstname != volunteer.Firstname)
            {
                found.Firstname = volunteer.Firstname;
            }

            if (!string.IsNullOrEmpty(volunteer.Lastname) && found.Lastname != volunteer.Lastname)
            {
                found.Lastname = volunteer.Lastname;
            }

            if (!string.IsNullOrEmpty(volunteer.Email) && found.Email != volunteer.Email)
            {
                found.Email = volunteer.Email;
            }

            if (!string.IsNullOrEmpty(volunteer.City) && found.City != volunteer.City)
            {
                found.City = volunteer.City;
            }

            if (volunteer.Status != null && found.Status != volunteer.Status)
            {
                found.Status = volunteer.Status;
            }

            if (volunteer.JoinedDate != null && found.JoinedDate != volunteer.JoinedDate)
            {
                found.JoinedDate = volunteer.JoinedDate;
            }

            // Save changes to the context
            _context.SaveChanges();
        }

        public int Count() 
        {
            int count = _context.BecomeVolunteers.Count();
            return count;
        }

    }
}