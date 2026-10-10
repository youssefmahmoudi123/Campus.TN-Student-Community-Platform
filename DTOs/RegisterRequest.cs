
namespace Campus.Api.DTOs;

// Data required to register a new Compus.tn account.
public class RegisterRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    // Academic affiliation selected during registration.
    public int UniversityId { get; set; }

    public int FacultyId { get; set; }
}