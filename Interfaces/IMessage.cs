using AsmvBackend.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces {
    public interface IMessage
    {
        bool AddMessage(MessageDTO message);
        List<MessageDTO> GetMessages();
        Message GetMessageById(long Id);
    }
}