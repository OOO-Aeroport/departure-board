using DepartureBoard.Application.Dto;

namespace DepartureBoard.Application.Ports.Network;

public interface ICheckInClient
{
    Task NotifyCheckInStart(int flightId, DateTime checkInEndTime);
    Task NotifyCheckInEnd(int flightId);
}