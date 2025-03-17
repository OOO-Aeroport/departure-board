namespace DepartureBoard.Application.Ports.Network.Factories;

public interface ICheckInClientFactory
{
    ICheckInClient? Require();
}