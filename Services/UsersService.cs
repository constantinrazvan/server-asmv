using ServerAsmv.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;

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

        public async Task<bool> ModifyPassword(long Id, string password)
        {
            User? user = await _context.Users.FindAsync(Id);
        
            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }
        
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
        
            if (BCrypt.Net.BCrypt.Verify(user.Password, password))
            {
                return false;
            }
        
            user.Password = hash;
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
            List<User> users = new List<User>();
        
            for(int i = 0; i < _context.Users.Count(); i++) {
                users.Add(_context.Users.ElementAt(i));
            }
        
            return users;
        }
    }
}