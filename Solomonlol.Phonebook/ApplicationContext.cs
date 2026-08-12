using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.Email, c.UserId })
                .IsUnique();
            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.PhoneNumber, c.UserId })
                .IsUnique();
            

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
