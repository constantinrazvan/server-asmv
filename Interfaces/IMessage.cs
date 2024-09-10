using AsmvBackend.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces {
    public interface IMessage
    {
        bool AddMessage(MessageDTO message);
        List<Message> GetMessages();
        Message GetMessageById(long Id);
    }
}