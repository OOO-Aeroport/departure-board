using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DepartureBoard.Api.Controllers;

[ApiController]
[Route("dep-board/api/v1/time")]
public class TimeController(TimeService timeService) : ControllerBase
{
    [HttpGet("now")]
    public IActionResult Get()
        => Ok(timeService.Now);

    [HttpGet("timeout")]
    public async Task<IActionResult> Timeout()
    {
        if (!HttpContext.Request.Query.TryGetValue("timeout", out var temp) ||
            !int.TryParse(temp, out var timeout))
        {
            return BadRequest("invalid timeout");
        }

        var afterTimeout = timeService.Now.AddSeconds(timeout);

        await Task.Run(async () =>
        {
            while (timeService.Now <= afterTimeout)
            {
                await Task.Delay(TimeSpan.FromMilliseconds((int)Constants.TickInMs));
            }
        });
        
        return Ok();
    }

    [HttpPut("spt")]
    public IActionResult SetSecondsPerTick()
    {
        if (!HttpContext.Request.Query.TryGetValue("spt", out var temp) ||
            !int.TryParse(temp, out var spt))
        {
            return BadRequest("invalid spt");
        }
        
        timeService.SecondsPerTick = spt;
        return Ok();
    }
}