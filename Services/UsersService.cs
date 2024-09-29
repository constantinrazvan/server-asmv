using ServerAsmv.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;
using BCrypt.Net;

namespace ServerAsmv.Services {
    public class UsersService : IUser {
        private readonly AppData _context; 

        public UsersService(AppData _context) { 
            this._context =_context;
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

        public async Task<bool> UpdateUser(long Id, User user)
        {
            User? userFound = await _context.Users.FindAsync(Id);

            if(userFound == null) {
                throw new KeyNotFoundException("User not found!");
            } 

            if(user.Firstname != userFound.Firstname && user.Firstname != null) {
                userFound.Firstname = user.Firstname;
            }

            if(user.Lastname != userFound.Lastname && user.Lastname != null) {
                userFound.Lastname = user.Lastname;
            }

            if(user.Role != userFound.Role && user.Role != null) {
                userFound.Role = user.Role;
            }

            _context.Update(userFound);
            await _context.SaveChangesAsync();

            return true;
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