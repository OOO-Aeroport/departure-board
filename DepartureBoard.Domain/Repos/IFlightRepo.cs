using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Domain.Repos;

public interface IFlightRepo
{
    Task CreateFlightAsync(Plane plane);
}