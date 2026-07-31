using backend.API.Extensions;
using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1")]
public class DestinationController(IDestinationService destinationService) : ControllerBase
{
    [HttpPost("trips/{tripId}/destinations")]
    public async Task<ActionResult<DestinationDto>> CreateDestinationForTrip(Guid tripId, [FromBody] CreateDestinationDto dto)
    {
        var result = await destinationService.CreateForTripAsync(tripId,dto);
        return result.ToActionResult();
    }

    [HttpPost("days/{dayId}/destinations/{destinationId}")]
    public async Task<ActionResult<DestinationDto>> CreateDestinationForDay(Guid dayId, [FromBody] CreateDestinationDto dto)
    {
        var result = await destinationService.CreateForDayAsync(dayId, dto);
        return result.ToActionResult();
    }

    [HttpDelete("destinations/{destinationId}")]
    public async Task<IActionResult> DeleteDestination(Guid destinationId)
    {
        var result = await destinationService.DeleteAsync(destinationId);
        return result.ToActionResult();
    }

    [HttpPatch("destinations/{destinationId}")]
    public async Task<ActionResult<DestinationDto>> UpdateDestination(Guid destinationId, [FromBody] UpdateDestinationDto dto)
    {
        var result = await destinationService.UpdateAsync(destinationId, dto);
        return result.ToActionResult();
    }
}
