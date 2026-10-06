using Campus.Api.DTOs;
using Campus.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Api.Controllers;


/// Handles user authentication operations (register and login).
/// Provides endpoints for user registration and authentication token generation.

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    /// Registers a new user account.
    /// Validates input and creates user in database. Returns user ID on success.
 
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);

            return Ok(new
            {
                message = "User registered successfully.",
                userId = user.Id
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }


    }

       /// Authenticates a user and issues authentication token.
       /// Validates credentials and returns token on success or 401 if invalid.
     
      [HttpPost("login")]
   public async Task<IActionResult> Login(LoginRequest request)
   {
       var result = await _authService.LoginAsync(request);

       if (result == null)
       {
           return Unauthorized(new
           {
               message = "Invalid email or password."
           });
       }

       return Ok(result);
   }
}