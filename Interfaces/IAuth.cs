using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces {
    public interface IAuth
    {
        bool register(RegisterDTO user);
        string login(LoginDTO user);
    }
}