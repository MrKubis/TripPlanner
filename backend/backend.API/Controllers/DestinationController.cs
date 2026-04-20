using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1")]
public class DestinationController : ControllerBase
{
    private readonly DestinationService _destinationService;

    public DestinationController(DestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpPost("trip/{tripId}/destination")]
    public async Task<ActionResult<DestinationDto>> CreateDestination(string tripId, [FromBody] CreateDestinationDto dto)
    {
        var result = await _destinationService.CreateForTrip(tripId,dto);
        return Ok(result);
    }

    [HttpDelete("trip/{tripId}/destination/{destinationId}")]
    public async Task<ActionResult> DeleteDestination(string tripId, string destinationId)
    {
        await _destinationService.DeleteForTrip(tripId, destinationId);
        return NoContent();
    }

    [HttpPatch("trip/{tripId}/destination/{destinationId}")]
    public async Task<ActionResult<DestinationDto>> UpdateDestination(string tripId, string destinationId,
        [FromBody] UpdateDestinationDto dto)
    {
        var result = await _destinationService.UpdateForTrip(tripId, destinationId, dto);
        return Ok(result);
    }

    [HttpPost("trip/{tripId}/day/{dayId}")]
    public async Task<ActionResult> CreateDay(string tripId, string dayId, [FromBody] CreateDestinationDto dto)
    {
        var result = await _destinationService.CreateForDay(tripId, dayId, dto);
        return Ok(result);
    }

    [HttpPatch("trip/{tripId}/day/{dayId}/append/destination/{id}")]
    public async Task<ActionResult> AppendDestinationToDay(string tripId, string dayId, string id)
    {
        await _destinationService.AppendDestinationToDay(tripId,dayId,id);
        return Ok();
    }

    [HttpPatch("trip/{tripId}/day/{dayId}/remove/destination/{id}")]
    public async Task<ActionResult> RemoveDestinationFromDay(string tripId, string dayId, string id)
    {
        await _destinationService.RemoveDestinationFromDay(tripId, dayId, id);
        return Ok();
    }
}
