using AsmvBackend.Models;
using ServerAsmv.Data;
using ServerAsmv.DTOs;
using ServerAsmv.Interfaces;

namespace ServerAsmv.Services {
    public class MessageService : IMessage
    {
        private readonly AppData _context;

        public MessageService(AppData context) { 
            _context = context;
        }

        public bool AddMessage(MessageDTO message)
        {
            if (message == null)
            {
                return false;
            }
        
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var newMessage = new Message{ 
                        Fullname = message.Fullname,
                        Email = message.Email,
                        Text = message.Text
                    };

                    _context.Messages.Add(newMessage);
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public Message GetMessageById(long Id)
        {
            if(Id == 0) {
                return null!;
            }

            Message found = _context.Messages.FirstOrDefault(m => m.Id == Id)!;            
            return found ?? throw new Exception("Message not found");
        }

        public List<Message> GetMessages()
        {
            var messages = _context.Messages.ToList();
        
            if (!messages.Any())
            {
                throw new Exception("No messages found");
            }
        
            return messages;
        }
    }
}