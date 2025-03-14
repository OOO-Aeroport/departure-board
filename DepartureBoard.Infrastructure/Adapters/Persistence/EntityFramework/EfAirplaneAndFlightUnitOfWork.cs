using DepartureBoard.App.Ports.Persistence;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Adapters.Persistence.EntityFramework;

public class EfAirplaneAndFlightUnitOfWork(AppDbContext context, IRepository<Airplane> airplaneRepository,
    IRepository<Flight> flightRepository) : IAirplaneAndFlightUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly IRepository<Airplane> _airplaneRepository = airplaneRepository;
    private readonly IRepository<Flight> _flightRepository = flightRepository;
    
    public async Task AddAirplaneAndFlightAsync(Airplane airplane, DateTime departureTime)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _airplaneRepository.AddAsync(airplane);
            var flight = new Flight
            {
                AirplaneId = airplane.Id,
                Airplane = airplane,
                DepartureTime = departureTime.ToUniversalTime()
            };
            await _flightRepository.AddAsync(flight);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}