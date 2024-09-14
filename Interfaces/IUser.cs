using AsmvBackend.Models;

namespace ServerAsmv.Interfaces { 
    public interface IUser { 
        User GetUser(long Id);
        Task<bool> ModifyPassword(long Id, string password);
        Task<bool> ModifyEmail(long Id, string email);
        Task<bool> DeleteUser(long Id);
        Task<bool> UpdateUser(long Id);
    }
}