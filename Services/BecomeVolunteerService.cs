﻿using ServerAsmv.DTOs;
using ServerAsmv.Models;
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
                readed: false
            );

            _context.BecomeVolunteers.Add(newVolunteer);
            _context.SaveChanges();
            
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

        public async Task<bool> UpdateBecomeVolunteer(BecomeVolunteerDTO newBecomeVolunteer, long Id)
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

        public async Task<string> MarkAsRead(long id) 
        { 
            var found = await _context.BecomeVolunteers.FindAsync(id);

            if (found == null) 
            {
                return $"The request with id {id} was not found.";
            }

            bool wasMarkedAsRead = found.Readed;
            found.Readed = !found.Readed;
            _context.Update(found);
            await _context.SaveChangesAsync(); 

            return wasMarkedAsRead ? "Marked as unread." : "Marked as read.";
        }

        public int Count() 
        {
            // Use LINQ Count method to get the number of entries
            int count = _context.BecomeVolunteers.Count();
            return count;
        }

        public bool Delete(long id) 
        {
            BecomeVolunteer? found = _context.BecomeVolunteers.Find(id);

            if (found == null) 
            {
                return false;
            }

            _context.BecomeVolunteers.Remove(found);

            int deletedCount = _context.SaveChanges();

            return deletedCount > 0;
        }
    }
}