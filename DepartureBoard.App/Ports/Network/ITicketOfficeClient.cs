namespace DepartureBoard.App.Ports.Network;

public interface ITicketOfficeClient
{
    Task Post(object dto);
}