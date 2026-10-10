
using System.Data;

namespace Campus.Api.Models;

// Represents a user account in Compus.tn.
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    // Every user belongs to a university and a faculty.
    public int UniversityId { get; set; }

    public int FacultyId { get; set; }

    // Every user has exactly one role.
    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSuspended { get; set; } = false;

    // Navigation properties: related database entities.
    public University University { get; set; } = null!;

    public Faculty Faculty { get; set; } = null!;

    public Role Role { get; set; } = null!;

    public StudentProfile? StudentProfile { get; set; }
}