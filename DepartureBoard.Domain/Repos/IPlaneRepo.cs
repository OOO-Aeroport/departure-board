using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Domain.Repos;

public interface IPlaneRepo
{
    Task AddRangeAsync(IEnumerable<Plane> planes);
}