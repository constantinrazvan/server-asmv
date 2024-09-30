using ServerAsmv.Models;

namespace ServerAsmv.Interfaces { 
    public interface IUser { 
        User GetUser(long Id);
        Task<bool> ModifyPassword(long Id, string password);
        Task<bool> ModifyEmail(long Id, string email);
        Task<bool> DeleteUser(long Id);
        Task<User> UpdateUser(long Id, User user);
    }
}