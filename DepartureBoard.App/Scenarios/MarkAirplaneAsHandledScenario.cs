using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;

namespace DepartureBoard.App.Scenarios;

public class MarkAirplaneAsHandledScenario(IRepository<Airplane> airplaneRepository)
{
    private readonly IRepository<Airplane> _airplaneRepository = airplaneRepository;

    public async Task Invoke(int id)
    {
        var airplane = await _airplaneRepository.GetByIdAsync(id);
        airplane.Handled = true;
        await _airplaneRepository.SaveChangesAsync();
    }
}