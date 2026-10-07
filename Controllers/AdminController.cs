using Campus.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Campus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Admin/users
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new
            {
                id = u.Id,
                firstName = u.FirstName,
                lastName = u.LastName,
                email = u.Email,
                role = u.Role,
                status = u.IsSuspended ? "SUSPENDED" : "ACTIVE",
                createdAt = u.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    // PUT: api/Admin/users/5/suspend
    [HttpPut("users/{id}/suspend")]
    public async Task<IActionResult> SuspendUser(int id)
    {
        var currentAdminId = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (currentAdminId == id.ToString())
        {
            return BadRequest(new
            {
                message = "You cannot suspend your own account."
            });
        }

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new
            {
                message = "User not found."
            });
        }

        user.IsSuspended = true;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "User suspended successfully."
        });
    }

    // PUT: api/Admin/users/5/activate
    [HttpPut("users/{id}/activate")]
    public async Task<IActionResult> ActivateUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new
            {
                message = "User not found."
            });
        }

        user.IsSuspended = false;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "User activated successfully."
        });
    }

    // DELETE: api/Admin/users/5
    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var currentAdminId = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (currentAdminId == id.ToString())
        {
            return BadRequest(new
            {
                message = "You cannot delete your own account."
            });
        }

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new
            {
                message = "User not found."
            });
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "User deleted successfully."
        });
    }
}
