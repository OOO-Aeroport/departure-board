using DepartureBoard.Domain.Entities;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfAirplaneAndFlightUnitOfWork(AppDbContext context, EfAirplaneRepository airplaneRepository, EfFlightRepository flightRepository)
{
    private readonly AppDbContext _context = context;
    private readonly EfAirplaneRepository _airplaneRepository = airplaneRepository;
    private readonly EfFlightRepository _flightRepository = flightRepository;
    
    public async Task CreateAirplaneAndFlight(Airplane airplane)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _airplaneRepository.AddAsync(airplane);
            //await _flightRepository.AddAsync();
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