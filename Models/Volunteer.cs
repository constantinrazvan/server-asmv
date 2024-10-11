using System;

namespace ServerAsmv.Models
{
    public class Volunteer
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string Ocupation { get; set; }
        public string JoinedDate { get; set; }
        public VolunteerImage VolunteerImage { get; set; }

        public Volunteer() { }

        public Volunteer(string firstname, string lastname, string email, string phone, string city, string status, string ocupation, string joinedDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Phone = phone;
            City = city;
            Status = status;
            Ocupation = ocupation;
            JoinedDate = joinedDate;
        }

        public override string ToString()
        {
            return $"Volunteer{{ id={Id}, firstname='{Firstname}', lastname='{Lastname}', email='{Email}', phone='{Phone}', city='{City}', status='{Status}', joinedDate='{JoinedDate}' }}";
        }
    }
}
