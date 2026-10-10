
using Campus.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Campus.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // University names must be unique.
        modelBuilder.Entity<University>()
            .HasIndex(u => u.Name)
            .IsUnique();

        // Faculty names must be unique within their university.
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => new { f.UniversityId, f.Name })
            .IsUnique();

        // Required as the target of the composite foreign key.
        modelBuilder.Entity<Faculty>()
            .HasAlternateKey(f => new { f.Id, f.UniversityId });

        // Each user belongs to one university.
        modelBuilder.Entity<User>()
            .HasOne(u => u.University)
            .WithMany()
            .HasForeignKey(u => u.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        // The selected faculty must belong to the selected university.
        modelBuilder.Entity<User>()
            .HasOne(u => u.Faculty)
            .WithMany()
            .HasForeignKey(u => new { u.FacultyId, u.UniversityId })
            .HasPrincipalKey(f => new { f.Id, f.UniversityId })
            .OnDelete(DeleteBehavior.Restrict);

        // Every user has exactly one role.
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Each user can have at most one student profile.
        modelBuilder.Entity<StudentProfile>()
            .HasKey(s => s.UserId);

        modelBuilder.Entity<StudentProfile>()
            .HasOne(s => s.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<StudentProfile>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Student numbers are unique when provided.
        modelBuilder.Entity<StudentProfile>()
            .HasIndex(s => s.StudentNumber)
            .IsUnique()
            .HasFilter("[StudentNumber] IS NOT NULL");

        // Role names must be unique.
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();

        // Email addresses must be unique.
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}