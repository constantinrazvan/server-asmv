using ServerAsmv.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;
using ServerAsmv.DTOs;

namespace ServerAsmv.Services {
    public class VolunteersService : IVolunteer
    {

        private readonly AppData _context;

        public VolunteersService(AppData _context) {
            this._context = _context;
        }

        public void AddVolunteer(VolunteerDTO volunteer)
        {
            if(volunteer == null) {
                throw new ArgumentNullException("Volunteer cannot be null!");
            }

            Volunteer newVolunteer = new Volunteer();

            newVolunteer.Firstname = volunteer.Firstname;
            newVolunteer.Lastname = volunteer.Lastname;
            newVolunteer.Email = volunteer.Email;
            newVolunteer.Status = volunteer.Status;
            newVolunteer.City = volunteer.City;
            newVolunteer.Phone = volunteer.Phone;
            newVolunteer.JoinedDate = volunteer.JoinedDate;

            _context.Volunteers.Add(newVolunteer);
            _context.SaveChanges();
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

        public Volunteer GetVolunteerById(long Id)
        {
            if(Id <= 0) {
                throw new ArgumentException("Id cannot be <= 0 !");
            }

            Volunteer? volunteer = _context.Volunteers.Find(Id);

            if(volunteer == null) {
                throw new KeyNotFoundException("Volunteer with id doesnt exist!");
            }

            return volunteer;
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

    }
}