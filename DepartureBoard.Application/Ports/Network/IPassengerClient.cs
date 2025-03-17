namespace DepartureBoard.Application.Ports.Network;

public interface IPassengerClient
{
    Task Post(object dto);
}