using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToothNet.Models;

namespace ToothNet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>()
            .HasOne(b => b.ApplicationUser)
            .WithMany()
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Treatment>()
            .HasOne(b => b.ApplicationUser)
            .WithMany()
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PatientResult>()
            .HasOne(b => b.ApplicationUser)
            .WithMany()
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<PatientResult> patientResults { get; set; }
    }
}