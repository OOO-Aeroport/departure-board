namespace DepartureBoard.Application.Ports.Network;

public interface ICheckInClient
{
    Task NotifyRegistrationStart(int flightId, DateTime checkInEndTime);
    Task NotifyRegistrationEnd(int flightId);
}