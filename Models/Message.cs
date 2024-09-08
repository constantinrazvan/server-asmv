using System;

namespace AsmvBackend.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool NewRequest { get; set; } = true;

        public Message() { }

        public Message(string name, string email, string text)
        {
            Name = name;
            Email = email;
            Text = text;
        }

        public override string ToString()
        {
            return $"Message{{ id={Id}, name='{Name}', email='{Email}', text='{Text}', newRequest={NewRequest} }}";
        }
    }
}