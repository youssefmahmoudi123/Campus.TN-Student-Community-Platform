using Campus.Api.DTOs;
using Campus.Api.Services;
using Campus.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Campus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AppDbContext _context;

    public UsersController(UserService userService, AppDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await _userService.GetCurrentUserAsync(
            int.Parse(userId));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var user = await _context.Users.FindAsync(int.Parse(userId));

        if (user == null)
            return NotFound(new { message = "User not found." });

        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email && u.Id != user.Id);

        if (emailExists)
            return BadRequest(new { message = "Email is already in use." });

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Profile updated successfully.",
            userId = user.Id,
            firstName = user.FirstName,
            lastName = user.LastName,
            email = user.Email,
            role = user.Role
        });
    }
}