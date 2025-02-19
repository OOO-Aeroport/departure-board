using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfPlaneRepo(AppDbContext context) : IPlaneRepo
{
    private readonly AppDbContext _context = context;

    public async Task AddRangeAsync(IEnumerable<Plane> planes)
        => await _context.Planes.AddRangeAsync(planes);
}