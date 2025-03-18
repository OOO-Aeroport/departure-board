namespace DepartureBoard.Application.Ports.Network;

public interface IPassengerClient
{
    Task NotifyFlightCreated(object dto);
    Task NotifyCheckInStart(int id, DateTime checkInEndTime);
    Task NotifyCheckInEnd(int id);
}