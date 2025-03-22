using DepartureBoard.Application.UseCases;
using DepartureBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DepartureBoard.Api.Controllers;

[ApiController]
[Route("dep-board/api/v1/airplanes")]
public class AirplaneController(CreateFlightUseCase createFlight,
    ScheduleCheckInUseCase scheduleCheckIn) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterFlight(Airplane airplane)
    {
        await createFlight.InvokeAsync(airplane);
        return Ok();
    }
    
    [HttpPost("{airplaneId:int}/ready")]
    public async Task<IActionResult> ScheduleCheckIn(int airplaneId)
    {
        await scheduleCheckIn.InvokeAsync(airplaneId);
        return Ok();
    }
}