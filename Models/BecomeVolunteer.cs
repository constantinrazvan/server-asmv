using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsmvBackend.Models
{
    [Table("become_volunteer")]
    public class BecomeVolunteer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Faculty { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; }

        public bool Readed { get; set; } = true;

        public BecomeVolunteer() { }

        public BecomeVolunteer(string email, string fullname, string phone, string faculty, string reason, bool readed)
        {
            Fullname = fullname;
            Email = email;
            Phone = phone;
            Faculty = faculty;
            Reason = reason;
            Readed = readed;
        }

        public override string ToString()
        {
            return $"BecomeVolunteer{{ Id={Id}, Fullname='{Fullname}', Email='{Email}', Phone='{Phone}', Faculty='{Faculty}', Reason='{Reason}', Readed={Readed} }}";
        }
    }
}
