using AsmvBackend.DTOs;
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

        public void AddBecomeVolunteer(BecomeVolunteerDTO newBecomeVolunteer)
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

            foreach (var becomeVolunteer in _context.BecomeVolunteers)
            {
                becomeVolunteers.Add(becomeVolunteer);
            }

            if (becomeVolunteers.Count == 0)
            {
                throw new KeyNotFoundException("No BecomeVolunteers found");
            }

            return becomeVolunteers;
        }

        public async Task<bool> UpdateBecomeVolunteer(BecomeVolunteer newBecomeVolunteer, long Id)
        {
            BecomeVolunteer? found = await _context.BecomeVolunteers.FindAsync(Id);
        
            if (found == null) {
                throw new KeyNotFoundException($"{Id} has not be found!");
            }
        
            if (newBecomeVolunteer.Fullname != found.Fullname || newBecomeVolunteer.Fullname != null) {
                found.Fullname = newBecomeVolunteer.Fullname;
            }
        
            if (newBecomeVolunteer.Email != found.Email || newBecomeVolunteer.Email != null) { 
                found.Email = newBecomeVolunteer.Email;
            }
        
            if (newBecomeVolunteer.Reason != found.Reason || newBecomeVolunteer.Reason != null) { 
                found.Reason = newBecomeVolunteer.Reason;
            }
        
            _context.Update(found);
            _context.SaveChanges();
            return true;
        }

        public async Task MarkAsRead(long Id) 
        { 
            BecomeVolunteer? found = await _context.BecomeVolunteers.FindAsync(Id);

            if(found == null) {
                throw new KeyNotFoundException($"The request with id {Id} was not found!");
            }

            found.NewRequest = !found.NewRequest;

            _context.Update(found);
            _context.SaveChanges();

            Console.WriteLine($"Request with ID {Id} has marked as read");
        }
    }
}