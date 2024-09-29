using ServerAsmv.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<ProjectImage> ProjectImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectImage)
                .WithOne(pi => pi.Project)
                .HasForeignKey<ProjectImage>(pi => pi.ProjectId);

            // Hash the password for the admin user
            var store_password = BCrypt.Net.BCrypt.HashPassword("adminasmv", BCrypt.Net.BCrypt.GenerateSalt());

            // Seed the database with an admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, // Asigură-te că Id-ul este unic și nu există un conflict
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Email = "admin@asmv.com",
                    Password = store_password,
                    Role = "admin",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
