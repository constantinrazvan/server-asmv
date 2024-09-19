using ServerAsmv.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerAsmv.Data
{
    public class AppData : DbContext
    {
        public AppData(DbContextOptions<AppData> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BecomeVolunteer> BecomeVolunteers { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}