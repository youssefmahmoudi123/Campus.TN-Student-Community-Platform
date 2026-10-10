using Campus.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FacultiesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Faculties?universityId=1
    [HttpGet]
    public async Task<IActionResult> GetFaculties(
        [FromQuery] int universityId)
    {
        if (universityId <= 0)
        {
            return BadRequest(new
            {
                message = "A valid universityId is required."
            });
        }

        var universityExists = await _context.Universities
            .AnyAsync(u => u.Id == universityId);

        if (!universityExists)
        {
            return NotFound(new
            {
                message = "University not found."
            });
        }

        var faculties = await _context.Faculties
            .AsNoTracking()
            .Where(f => f.UniversityId == universityId)
            .OrderBy(f => f.Name)
            .Select(f => new
            {
                f.Id,
                f.Name,
                f.UniversityId
            })
            .ToListAsync();

        return Ok(faculties);
    }
}