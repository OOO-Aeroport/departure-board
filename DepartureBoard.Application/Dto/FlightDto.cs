namespace DepartureBoard.Application.Dto;

public record FlightDto(int FlightId, int AirplaneId, int SeatsAvailable, int BaggageAvailable);