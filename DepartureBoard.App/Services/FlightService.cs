using DepartureBoard.Domain.Entities;

namespace DepartureBoard.App.Services;

public class FlightService(IAirplaneAndFlightUnitOfWork unitOfWork)
{
    private readonly IAirplaneAndFlightUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task RegisterFlight(Airplane plane, DateTime departureTime) 
        => await _unitOfWork.AddAirplaneAndFlightAsync(plane, departureTime);
}