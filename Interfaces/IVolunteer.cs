using ServerAsmv.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces { 
    public interface IVolunteer
    {
        List<Volunteer> GetVolunteers();
        Task AddVolunteer(VolunteerDTO volunteer, IFormFile photo);
        void DeleteVolunteer(long Id);
        void UpdateVolunteer(long Id, Volunteer volunteer);
        Volunteer GetVolunteerById(long Id);
    }
}