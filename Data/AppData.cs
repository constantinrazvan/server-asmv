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
                
            var store_password = BCrypt.Net.BCrypt.HashPassword("adminasmv", BCrypt.Net.BCrypt.GenerateSalt());

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

            var secondUserPass = BCrypt.Net.BCrypt.HashPassword("razvan20", BCrypt.Net.BCrypt.GenerateSalt());
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2, 
                    Firstname = "Razvan", 
                    Lastname = "Constantin", 
                    Email = "razvanpana20@gmail.com", 
                    Password = secondUserPass,
                    Role = "Membru Voluntar", 
                    CreatedAt = DateTime.UtcNow, 
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
