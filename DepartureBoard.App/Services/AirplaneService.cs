using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;

namespace DepartureBoard.App.Services;

public class AirplaneService(IRepository<Airplane> airplaneRepository)
{
    private readonly IRepository<Airplane> _airplaneRepository = airplaneRepository;

    public async Task MarkAsHandledAsync(int id)
    {
        var airplane = await _airplaneRepository.GetByIdAsync(id);
        airplane.Handled = true;
        await _airplaneRepository.SaveChangesAsync();
    }
}