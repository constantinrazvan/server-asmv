using ServerAsmv.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces {
    public interface IMessage
    {
        bool AddMessage(Message message);
        Stack<Message> GetMessages();
        Message GetMessageById(long Id);
    }
}