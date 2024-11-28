using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAsmv.Models {
    [Table("volunteers")]
    public class Volunteer { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set;}
        public string Firstname {get; set;}
        public string Lastname {get; set;} 
        public string Email {get; set;} 
        public string JoinedDate {get; set;}
        public string Ocupation { get; set;}
        public bool President {get; set;}
        public bool VicePresident {get; set;}
        public bool Secretary { get; set; }
        public string Department {get; set;}
        public VolunteerImage? VolunteerImage {get; set;}
        public string PhoneNumber {get; set;}
    
        public Volunteer() {}

        public Volunteer(string firstname, string lastname, string email, string joinedDate, string ocupation, string department, bool president, bool vicepresident, string phoneNumber)
        {
            Firstname = firstname;
            Lastname = lastname; 
            Email = email;
            JoinedDate = joinedDate;
            Ocupation = ocupation;
            President = president; 
            VicePresident = vicepresident;
            Department = department;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"Volunteer: {Firstname} {Lastname}, Email: {Email}, Joined Date: {JoinedDate}, Ocupation: {Ocupation}, President: {President}, Vice President: {VicePresident}, Department: {Department}";
        }

    }
}