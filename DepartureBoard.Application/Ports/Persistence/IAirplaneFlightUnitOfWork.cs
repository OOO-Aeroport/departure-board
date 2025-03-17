using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.Ports.Persistence;

public interface IAirplaneFlightUnitOfWork
{
    Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime);
}