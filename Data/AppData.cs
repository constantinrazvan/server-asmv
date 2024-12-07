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
        public DbSet<VolunteerImage> VolunteerImages { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectImage)
                .WithOne(pi => pi.Project)
                .HasForeignKey<ProjectImage>(pi => pi.ProjectId);

            modelBuilder.Entity<Volunteer>()
                .HasOne(v => v.VolunteerImage)
                .WithOne(vi => vi.Volunteer)
                .HasForeignKey<VolunteerImage>(vi => vi.VolunteerId);

            // Generare hash pentru parola utilizatorului admin
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("adminasmv", BCrypt.Net.BCrypt.GenerateSalt());

            // Adăugare utilizator admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, // ID-ul utilizatorului trebuie să fie unic
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Email = "admin@asmv.com",
                    Password = adminPassword,
                    Role = "admin",
                    CreatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<ActivityUser>()
                .HasKey(au => new { au.ActivityId, au.UserId });

            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.Activity)
                .WithMany(a => a.AssignedUsers)
                .HasForeignKey(au => au.ActivityId);

            modelBuilder.Entity<ActivityUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.AssignedActivities)
                .HasForeignKey(au => au.UserId);
        }
    }
}
