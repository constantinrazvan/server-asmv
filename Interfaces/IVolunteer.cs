using ServerAsmv.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces { 
    public interface IVolunteer
    {
        List<Volunteer> GetVolunteers();
        void AddVolunteer(VolunteerDTO volunteer);
        void DeleteVolunteer(long Id);
        void UpdateVolunteer(long Id, Volunteer volunteer);
        Volunteer GetVolunteerById(int id);
    }
}