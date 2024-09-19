using ServerAsmv.Models; // Ensure this namespace contains the User class
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using ServerAsmv.Data;
using ServerAsmv.Utils;
using System.Linq; // Ensure you have the correct LINQ namespace

namespace ServerAsmv.Services
{
    public class AuthService : IAuth
    {
        private readonly AppData _dbContext; 
        private readonly JwtUtil jwtUtil;

        public AuthService(AppData dbContext, JwtUtil jwtUtil) {
            this._dbContext = dbContext;
            this.jwtUtil = jwtUtil;
        }
        
        public string login(LoginDTO user)
        {
            User? find = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if(find == null) {
                return "User not found";
            }

            if(!BCrypt.Net.BCrypt.Verify(user.Password, find.Password)) {
                return "Wrong password";
            }

            string token = jwtUtil.GenerateToken(find.Id.ToString(), find.Firstname, find.Lastname, find.Email, find.Role);        

            return token;
        }

        public bool register(RegisterDTO user)
        {
            User? find = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if(find != null) { 
                return false;
            }

            User newUser = new User(); // Ensure the correct class is used
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());

            newUser.Firstname = user.Firstname;
            newUser.Lastname = user.Lastname;
            newUser.Email = user.Email;
            newUser.Password = password;
            newUser.Role = user.Role;
            newUser.CreatedAt = DateTime.Now;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return true;
        }

        public User? GetProfile(long Id) // Return User? for nullability
        { 
            User? found = _dbContext.Users.Find(Id);

            return found;
        }
    }
}
