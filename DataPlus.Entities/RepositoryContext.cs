using DataPlus.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataPlus.Entities
{
    public class RepositoryContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Signature> Signatures { get; set; }

        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .IsRequired();

            modelBuilder.Entity<EnrollmentStudent>()
                .HasKey(es => new { es.EnrollmentId, es.StudentId });
            modelBuilder.Entity<EnrollmentStudent>()
                .HasOne(es => es.Enrollment)
                .WithMany(e => e.EnrollmentStudents)
                .HasForeignKey(es => es.EnrollmentId);
            modelBuilder.Entity<EnrollmentStudent>()
                .HasOne(es => es.Student)
                .WithMany(s => s.EnrollmentStudents)
                .HasForeignKey(es => es.StudentId);

            modelBuilder.Entity<EnrollmentSignature>()
                .HasKey(es => new { es.EnrollmentId, es.SignatureId });
            modelBuilder.Entity<EnrollmentSignature>()
                .HasOne(es => es.Enrollment)
                .WithMany(e => e.EnrollmentSignatures)
                .HasForeignKey(es => es.EnrollmentId);
            modelBuilder.Entity<EnrollmentSignature>()
                .HasOne(es => es.Signature)
                .WithMany(s => s.EnrollmentSignatures)
                .HasForeignKey(es => es.SignatureId);

        }

    }
}
