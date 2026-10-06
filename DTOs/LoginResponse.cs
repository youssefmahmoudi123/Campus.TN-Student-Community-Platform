namespace Campus.Api.DTOs;

// Data transfer object returned after successful login. Contains JWT token and user profile info.
public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}