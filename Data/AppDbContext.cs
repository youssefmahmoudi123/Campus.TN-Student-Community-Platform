using Campus.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus.Api.Data;

// Entity Framework Core database context. Manages database connection and User entity mapping.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}