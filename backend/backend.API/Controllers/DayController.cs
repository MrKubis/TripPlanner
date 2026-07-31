using backend.API.Extensions;
using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1")]
public class DayController(IDayService dayService): ControllerBase
{
    [HttpPost("trips/{tripId}/days")]
    public async Task<ActionResult<DayDto>> Create([FromRoute] Guid tripId, [FromBody] CreateDayDto dto)
    {
        var result = await dayService.CreateForTripAsync(tripId, dto);
        return result.ToActionResult();
    }

    [HttpPatch("days/{dayId}")]
    public async Task<ActionResult<DayDto>> Update([FromRoute] Guid dayId, [FromBody] UpdateDayDto dto)
    {
        var result = await dayService.UpdateAsync(dayId, dto);
        return result.ToActionResult();
    }
    
    [HttpDelete("days/{dayId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid dayId)
    {
        var result = await dayService.DeleteAsync(dayId);
        return result.ToActionResult();
    }

    [HttpPut("days/{dayId}/appendDestination/{destinationId}")]
    public async Task<IActionResult> AppendDestination([FromRoute] Guid dayId, [FromRoute] Guid destinationId)
    {
        var result = await dayService.AppendDestinationAsync(dayId, destinationId);
        return result.ToActionResult();
    }
}
