using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;

namespace DepartureBoard.App.Services;

public class PlaneService(IPlaneRepo repo)
{
    private readonly IPlaneRepo _repo = repo;
    
    public async Task AddManyAsync(IEnumerable<Plane> planes)
    {
        await _repo.AddRangeAsync(planes);
        
    }
}