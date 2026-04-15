using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1")]
public class DayController : ControllerBase
{
    private readonly DayService _dayService;

    public DayController(DayService dayService)
    {
        _dayService = dayService;
    }

    [HttpPost("trip/{tripId}/day")]
    public async Task<ActionResult<DayDto>> Create([FromRoute] string tripId, [FromBody] CreateDayDto dto)
    {
        var result = await _dayService.CreateForTrip(tripId, dto);
        return Ok(result);
    }

    [HttpPatch("trip/{tripId}/day/{dayId}")]
    public async Task<ActionResult<DayDto>> Update([FromRoute] string tripId, [FromRoute] string dayId, [FromBody] UpdateDayDto dto)
    {
        var result = await  _dayService.UpdateForTrip(tripId, dayId, dto);
        return Ok(result);
    }
}
