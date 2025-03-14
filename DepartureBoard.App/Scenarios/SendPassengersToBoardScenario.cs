using DepartureBoard.App.Ports.Network;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;

namespace DepartureBoard.App.Scenarios;

public class SendPassengersToBoardScenario(IRepository<Flight> flightRepository, IBoardClient board)
{
    private readonly IRepository<Flight> _flightRepository = flightRepository;
    private readonly IBoardClient _board = board;
    
    public async Task Invoke(int flightId, List<object> passengers)
    {
        var flight = await _flightRepository.GetByIdAsync(flightId);
        
        if (flight == null) throw new NullReferenceException("Flight not found");
        
        _ = _board.Post(flight.AirplaneId, passengers);
    }
}