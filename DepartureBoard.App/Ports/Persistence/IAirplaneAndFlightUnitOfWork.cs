using DepartureBoard.Domain.Entities;

namespace DepartureBoard.App.Ports.Persistence;

public interface IAirplaneAndFlightUnitOfWork
{
    Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime);
}