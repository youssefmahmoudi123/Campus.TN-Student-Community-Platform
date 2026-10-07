namespace Campus.Api.Models;

// Represents a user entity in the database. Stores profile, hashed password, role, and creation timestamp.
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "Student";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSuspended { get; set; } = false;
}