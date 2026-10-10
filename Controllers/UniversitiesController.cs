using Campus.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public UniversitiesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Universities
    [HttpGet]
    public async Task<IActionResult> GetUniversities()
    {
        var universities = await _context.Universities
            .AsNoTracking()
            .OrderBy(u => u.Name)
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Location
            })
            .ToListAsync();

        return Ok(universities);
    }
}