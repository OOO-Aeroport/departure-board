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
                await Task.Delay(TimeSpan.FromMilliseconds((int)Constants.TickInMilliseconds));
            }
        });
        
        return Ok(timeService.Now);
    }

    [HttpPut("tps")]
    public IActionResult SetTicksPerSecond()
    {
        if (!HttpContext.Request.Query.TryGetValue("tps", out var temp) ||
            !int.TryParse(temp, out var tps))
        {
            return BadRequest("invalid tps");
        }
        
        timeService.TicksPerSecond = tps;
        return Ok();
    }
}