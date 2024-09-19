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
            throw new NotImplementedException();
        }

        public void DeleteVolunteer(long Id)
        {
            throw new NotImplementedException();
        }

        public Volunteer GetVolunteerById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Volunteer> GetVolunteers()
        {
            throw new NotImplementedException();
        }

        public void UpdateVolunteer(long Id, Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
    }
}