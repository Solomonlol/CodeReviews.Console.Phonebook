using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace Solomonlol.ShiftLogger
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Shift> Shifts { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shift>()
                .HasOne(u=>u.User)
                .WithMany(s=>s.Shifts)
                .HasForeignKey(s=>s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserIdentificationNumber)
                .IsUnique();
        }
    }
}
