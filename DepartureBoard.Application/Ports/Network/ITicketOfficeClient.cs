using DepartureBoard.Application.Dto;

namespace DepartureBoard.Application.Ports.Network;

public interface ITicketOfficeClient
{
    Task Post(FlightDto dto);
}