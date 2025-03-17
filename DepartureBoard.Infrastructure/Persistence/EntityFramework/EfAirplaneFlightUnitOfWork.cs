using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfAirplaneFlightUnitOfWork(IAirplaneRepository airplaneRepository,
    IFlightRepository flightRepository, AirplaneFlightDbContext context) : IAirplaneFlightUnitOfWork
{
    private readonly IAirplaneRepository _airplaneRepository = airplaneRepository;
    private readonly IFlightRepository _flightRepository = flightRepository;
    private readonly AirplaneFlightDbContext _context = context;

    public async Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _airplaneRepository.AddAsync(airplane);
            var flight = new Flight
            {
                DepartureTime = departureTime.ToUniversalTime(),
                AirplaneId = airplane.Id
            };
            await _flightRepository.AddAsync(flight);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}