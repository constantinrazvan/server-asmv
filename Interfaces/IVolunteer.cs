using AsmvBackend.Models;

namespace ServerAsmv.Interfaces { 
    public interface IVolunteer
    {
        List<Volunteer> GetVolunteers();
        void AddVolunteer(Volunteer volunteer);
        void DeleteVolunteer(long Id);
        void UpdateVolunteer(long Id, Volunteer volunteer);
        Volunteer GetVolunteerById(int id);
    }
}