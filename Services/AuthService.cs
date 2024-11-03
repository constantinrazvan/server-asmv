using ServerAsmv.Models; // Ensure this namespace contains the User class
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;
using ServerAsmv.Data;
using ServerAsmv.Utils;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Ensure you have the correct LINQ namespace

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

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public string? login(LoginDTO user)
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            User? find = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (find == null)
            {
                return null; // User not found
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, find.Password))
            {
                return null; // Wrong password
            }

            string token = jwtUtil.GenerateToken(find.Id.ToString(), find.Firstname, find.Lastname, find.Email, find.Role, find.CreatedAt);
            return token; // Return the generated token
        }

        public bool register(RegisterDTO user)
        {
            User? find = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if(find != null) { 
                return false;
            }

            if(user.AccessKey != "asmv2024!#") {
                return false;
            }

            User newUser = new User(); // Ensure the correct class is used
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());

            newUser.Firstname = user.Firstname;
            newUser.Lastname = user.Lastname;
            newUser.Email = user.Email;
            newUser.Password = password;
            newUser.Role = user.Role;
            newUser.CreatedAt = DateTime.UtcNow;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return true;
        }

        public User? GetProfile(long Id) // Return User? for nullability
        { 
            User? found = _dbContext.Users.Find(Id);

            return found;
        }

        public async Task<(bool Success, string Message)> ChangePassword(long Id, ChangePasswordDTO requestPass)
        {
            try
            {
                User? find = await _dbContext.Users.FindAsync(Id);
                
                if (find == null)
                {
                    return (false, "Utilizatorul nu a fost găsit.");
                }

                bool checkPasswords = BCrypt.Net.BCrypt.Verify(requestPass.CurrentPassword, find.Password);

                if (!checkPasswords)
                {
                    return (false, "Parola curentă este incorectă.");
                }

                find.Password = BCrypt.Net.BCrypt.HashPassword(requestPass.NewPassword); // Hash the new password
                
                _dbContext.Users.Update(find);
                await _dbContext.SaveChangesAsync();

                return (true, "Parola a fost actualizată cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea parolei: {ex.Message}");
                return (false, $"A apărut o eroare la actualizarea parolei: {ex.Message}");
            }
        }
    }
}