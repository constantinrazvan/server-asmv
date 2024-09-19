using System;

namespace ServerAsmv.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool NewRequest { get; set; } = true;

        public Message() { }

        public Message(string name, string email, string text)
        {
            Fullname = name;
            Email = email;
            Text = text;
        }

        public override string ToString()
        {
            return $"Message{{ id={Id}, Fullname='{Fullname}', email='{Email}', text='{Text}', newRequest={NewRequest} }}";
        }
    }
}