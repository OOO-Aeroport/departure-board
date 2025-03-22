using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfAirplaneFlightUnitOfWork(IAirplaneRepository airplaneRepository,
    IFlightRepository flightRepository, AirplaneFlightDbContext context) : IAirplaneFlightUnitOfWork
{
    public async Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await airplaneRepository.AddAsync(airplane);
            var flight = new Flight
            {
                DepartureTime = departureTime.ToUniversalTime(),
                AirplaneId = airplane.Id
            };
            await flightRepository.AddAsync(flight);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}