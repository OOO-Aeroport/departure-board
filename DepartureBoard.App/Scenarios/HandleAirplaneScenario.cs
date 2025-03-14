using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;

namespace DepartureBoard.App.Scenarios;

public class HandleAirplaneScenario(IRepository<Airplane> airplaneRepository)
{
    private readonly IRepository<Airplane> _airplaneRepository = airplaneRepository;

    public async Task Invoke(int id)
    {
        var airplane = await _airplaneRepository.GetByIdAsync(id);
        if (airplane == null) throw new NullReferenceException("Airplane not found");
        airplane.Handled = true;
        
        await _airplaneRepository.SaveChangesAsync();
    }
}