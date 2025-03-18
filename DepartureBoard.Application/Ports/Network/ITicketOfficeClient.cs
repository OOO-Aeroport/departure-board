namespace DepartureBoard.Application.Ports.Network;

public interface ITicketOfficeClient
{
    Task NotifyFlightCreated(object dto);
}