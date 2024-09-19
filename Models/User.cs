using System;

namespace ServerAsmv.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        // Constructor fără parametri
        public User() { }

        // Constructor cu parametri
        public User(string firstname, string lastname, string email, string password, string role, DateTime createdAt)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = createdAt;
        }

        // Suprascrierea metodei ToString pentru a afișa obiectul
        public override string ToString()
        {
            return $"User{{ id={Id}, firstname='{Firstname}', lastname='{Lastname}', email='{Email}', password='******', role='{Role}', createdAt={CreatedAt} }}";
        }
    }
}