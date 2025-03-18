namespace DepartureBoard.Application.Ports.Network;

public interface IPassengerClient
{
    Task NotifyFlightCreated(object dto);
    Task NotifyCheckInStart(int flightId, DateTime checkInEndTime);
    Task NotifyCheckInEnd(int flightId);
}