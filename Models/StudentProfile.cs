
namespace Campus.Api.Models;

// Student-specific details, kept separate from general account information.
public class StudentProfile
{
    // The user's ID is also the primary key of this profile.
    public int UserId { get; set; }

    // Optional because not every student has provided a student number.
    public string? StudentNumber { get; set; }

    // Navigation property back to the associated user.
    public User User { get; set; } = null!;
}