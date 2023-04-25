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

            builder.Entity<PatientPhoto>()
            .HasOne(b => b.ApplicationUser)
            .WithMany()
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PatientProblem>()
            .HasOne(b => b.ApplicationUser)
            .WithMany()
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientPhoto> PatientPhotos { get; set; }
        public DbSet<PatientProblem> PatientProblems { get; set; }
    }
}