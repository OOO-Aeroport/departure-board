using DepartureBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DepartureBoard.Api.Controllers;

[ApiController]
[Route("dep-board/api/v1/time")]
public class TimeController(TimeService timeService) : ControllerBase
{
    [HttpGet("now")]
    public IActionResult Get()
    {
        return Ok(timeService.Now);
    }

    [HttpPut("tps")]
    public IActionResult SetTicksPerSecond()
    {
        if (!HttpContext.Request.Query.TryGetValue("tps", out var temp) ||
            !int.TryParse(temp, out var tps))
        {
            return BadRequest();
        }
        
        timeService.TicksPerSecond = tps;
        return Ok();
    }
}