using DepartureBoard.Application.UseCases;
using DepartureBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DepartureBoard.Api.Controllers;

[ApiController]
[Route("dep-board/api/v1/airplanes")]
public class AirplaneController(ILogger<AirplaneController> logger,
    RegisterFlightUseCase registerFlight, ScheduleCheckInUseCase scheduleCheckIn) : ControllerBase
{
    private readonly ILogger<AirplaneController> _logger = logger;
    private readonly RegisterFlightUseCase _registerFlight = registerFlight;
    private readonly ScheduleCheckInUseCase _scheduleCheckIn = scheduleCheckIn;
    
    [HttpPost]
    public async Task<IActionResult> RegisterFlight(Airplane airplane)
    {
        await _registerFlight.InvokeAsync(airplane);
        return Ok();
    }
    
    [HttpPost("{airplaneId:int}/ready")]
    public async Task<IActionResult> ScheduleCheckIn(int airplaneId)
    {
        await _scheduleCheckIn.InvokeAsync(airplaneId);
        return Ok();
    }
}