using AsmvBackend.Models;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;

namespace ServerAsmv.Services {
    public class MessageService : IMessage
    {
        public bool AddMessage(MessageDTO message)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageById(long Id)
        {
            throw new NotImplementedException();
        }

        public List<MessageDTO> GetMessages()
        {
            throw new NotImplementedException();
        }
    }
}