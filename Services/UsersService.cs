using ServerAsmv.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;
using BCrypt.Net;

namespace ServerAsmv.Services {
    public class UsersService : IUser {
        private readonly AppData _context; 
        private readonly ILogger<UsersService> _logger;

        public UsersService(AppData _context, ILogger<UsersService> logger) { 
            this._context =_context;
            this._logger = logger;
        }

        public async Task<bool> DeleteUser(long Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Id), "Provide a higher ID!");
            }
        
            User? user = await _context.Users.FindAsync(Id);
        
            if (user == null)
            {
                throw new KeyNotFoundException("ID was not found!");
            }
        
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public User GetUser(long Id)
        {
            User? user = _context.Users.Find(Id);

            if(user == null) {
                throw new KeyNotFoundException("User not found!");
            }

            return user;
        }

        public async Task<bool> ModifyEmail(long Id, string email)
        {
            User? user = await _context.Users.FindAsync(Id);
        
            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }
        
            if (user.Email != email)
            {
                user.Email = email;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
        
            return false;
        }

        public async Task<bool> ModifyPassword(long Id, string newPassword)
        {
            User? user = await _context.Users.FindAsync(Id);
            
            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            // Hash the new password
            string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt());

            // Update the user's password
            user.Password = newHash;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> UpdateUser(long id, User user)
        {
           User? found = await _context.Users.FindAsync(id);

           if(found == null) 
           {
            return null!;
           }

           if(found.Firstname != user.Firstname && user.Firstname != null){
                found.Firstname = user.Firstname;
           } else {
                found.Firstname = found.Firstname;
           }

           if(found.Lastname != user.Lastname && user.Lastname != null) {
                found.Lastname = user.Lastname;      
           } else {
                found.Lastname = found.Lastname;
           }

           if(found.Email != user.Email && user.Email != null) {
                found.Email = user.Email;
           } else {
                found.Email = found.Email;
           }

           if(found.Role != user.Role && user.Role != null) {
                found.Role = user.Role;
           } else { 
                found.Role = found.Role;
           }

           _context.Update(found);
           _context.Users.Update(found);
           await _context.SaveChangesAsync();

           User? updatedUser = await _context.Users.FindAsync(id);

           return updatedUser!;
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public List<User> GetAll()
        {
        
            return _context.Users.ToList();
        }
    }
}