using DepartureBoard.Domain.Entities;

namespace DepartureBoard.App;

public interface IAirplaneAndFlightUnitOfWork
{
    Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime);
}