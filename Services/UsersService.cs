using AsmvBackend.Models;
using ServerAsmv.Data;
using ServerAsmv.Interfaces;

namespace ServerAsmv.Services {
    public class UsersService : IUser {
        private readonly AppData _context; 

        public UsersService(AppData _context) { 
            this._context =_context;
        }

        public Task<bool> DeleteUser(long Id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyEmail(long Id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyPassword(long Id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(long Id)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            int number = 0;

            foreach(User u in _context.Users) {
                number++;
            }

            return number;
        }
    }
}