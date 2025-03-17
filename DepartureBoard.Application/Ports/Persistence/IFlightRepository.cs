using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.Ports.Persistence;

public interface IFlightRepository
{
    Task AddAsync(Flight flight);
    Task SaveChangesAsync();
    Task<Flight?> FindByAirplaneIdAsync(int airplaneId);
}