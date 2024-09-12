using AsmvBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;

namespace ServerAsmv.Services {
    public class BecomeVolunteerService : IBecomeVolunteer
    {
        private readonly AppData _context;

        public BecomeVolunteerService(AppData context)
        {
            _context = context;
        }

        public void AddBecomeVolunteer(BecomeVolunteer newBecomeVolunteer)
        {
            if(newBecomeVolunteer == null) {
                throw new ArgumentNullException("Request cannot be null!");
            }

            BecomeVolunteer newVolunteer = new BecomeVolunteer(
                fullname: newBecomeVolunteer.Fullname,
                email: newBecomeVolunteer.Email,
                phone: newBecomeVolunteer.Phone,
                faculty: newBecomeVolunteer.Faculty,
                reason: newBecomeVolunteer.Reason,
                newRequest: true
            );

            _context.BecomeVolunteers.Add(newVolunteer);
            Console.WriteLine("newBecomeVolunteer has been added");
            Console.WriteLine($"{newBecomeVolunteer}");
        }

        public async void DeleteBecomeVolunteer(long Id)
        {
            BecomeVolunteer? found = await _context.BecomeVolunteers.FindAsync(Id);

            if(found == null) {
                throw new KeyNotFoundException($"User with ID {Id} has not be found!");
            }

            _context.BecomeVolunteers.Remove(found);
            _context.SaveChanges();
        }

        public BecomeVolunteer GetBecomeVolunteer(long Id)
        {
            BecomeVolunteer found = _context.BecomeVolunteers.FindAsync(Id).Result!;

            if(found == null) {
                throw new KeyNotFoundException("BecomeVolunteer not found");
            }

            return found;
        }

        public List<BecomeVolunteer> GetBecomeVolunteers()
        {
            List<BecomeVolunteer> becomeVolunteers = new List<BecomeVolunteer>();

            for(int i = 0; i < _context.BecomeVolunteers.Count(); i++) {
                becomeVolunteers.Add(_context.BecomeVolunteers.ElementAt(i));
            }

            if(becomeVolunteers.Count == 0) {
                throw new KeyNotFoundException("No BecomeVolunteers found");
            }

            return becomeVolunteers;
        }

        public bool UpdateBecomeVolunteer(BecomeVolunteer newBecomeVolunteer)
        {
            throw new NotImplementedException();
        }
    }
}