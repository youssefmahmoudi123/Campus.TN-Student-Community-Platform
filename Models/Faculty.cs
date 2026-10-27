
namespace Campus.Api.Models;

public class Faculty
{
    public        int Id { get; set; }

    public       string Name { get; set; } = string.Empty;

    public int UniversityId { get; set; }

    public University University { get; set; } = null!;
}

