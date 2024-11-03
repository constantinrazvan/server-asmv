using ServerAsmv.DTOs;
using ServerAsmv.Models;

namespace ServerAsmv.Interfaces {
    public interface IVolunteer { 
        public Task<string> AddVolunteer(VolunteerDTO newVolunteer, IFormFile photo);
        public Task<List<Volunteer>> GetVolunteers();
        public Volunteer? GetVolunteer(long id);
        public Task<bool> UpdateVolunteer(long id, VolunteerDTO volunteer, IFormFile photo);
        public Task<(bool, string message)> RemoveVolunteer(long id);
        public int Count();
    }
}