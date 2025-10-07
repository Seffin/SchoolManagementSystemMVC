using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemMVC.Models;

namespace SchoolManagementSystemMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Qualifications)
                .WithOne(q => q.Student)
                .HasForeignKey(q => q.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Qualification>()
        .HasOne(q => q.Student)
        .WithMany(s => s.Qualifications)
        .HasForeignKey(q => q.StudentId)
        .IsRequired(false);
        }
    }
}