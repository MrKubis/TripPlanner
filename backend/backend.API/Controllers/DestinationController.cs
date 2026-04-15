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
}
