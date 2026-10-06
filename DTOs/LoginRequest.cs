namespace Campus.Api.DTOs;

// Data transfer object for login endpoint. Contains email and password credentials.
public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}