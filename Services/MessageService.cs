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

        public bool AddMessage(Message message)
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

        public Stack<Message> GetMessages()
        {
            Stack<Message> messages = new Stack<Message>();

            var allMessages = _context.Messages.ToList();
            for (int i = allMessages.Count - 1; i >= 0; i--)
            {
                messages.Push(allMessages[i]);
            }

            return messages;
        }

        public int Count()
        {
            return _context.Messages.Count();
        }

        public async Task<bool> MarkAsReadAsync(long id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null) 
            {
                throw new KeyNotFoundException($"Sorry, but message with ID {id} was not found.");
            }

            message.NewRequest = !message.NewRequest;

            _context.Messages.Update(message);
            await _context.SaveChangesAsync();

            return message.NewRequest;
        }

    }
}