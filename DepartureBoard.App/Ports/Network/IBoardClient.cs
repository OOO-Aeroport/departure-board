using DepartureBoard.App.Scenarios;

namespace DepartureBoard.App.Ports.Network;

public interface IBoardClient
{
    Task Post(int id, List<object> dtos);
}