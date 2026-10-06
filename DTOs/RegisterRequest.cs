namespace Campus.Api.DTOs;

// Data transfer object for user registration endpoint. Contains user profile and password.
public class RegisterRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}