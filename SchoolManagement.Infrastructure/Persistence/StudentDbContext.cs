using System;
using Microsoft.EntityFrameworkCore;
using SchoolManagment.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence
{
    public class StudentDbContext : DbContext
    {



        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Section)
                .WithMany(sec => sec.Students)
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

