using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api")]
public class DestinationController : BaseApiController
{
    private readonly DestinationService _service;

    public DestinationController(DestinationService service)
    {
        _service = service;
    }

    [HttpPost("trip/{tripId}/destination")]
    public async Task<IActionResult> CreateForTrip(string tripId, [FromBody] CreateDestinationDto dto)
    {
        await _service.CreateForTrip(tripId, dto);
        return Created();
    }

    [HttpDelete("trip/{tripId}/destination/{id}")]
    public async Task<IActionResult> DeleteForTrip(string tripId, string id)
    {
        await _service.DeleteForTrip(tripId,id);
        return NoContent();
    }
    [HttpPatch("/trip/{tripId}/destination/{id}")]
    public async Task<IActionResult> UpdateForTrip(string tripId,string id, [FromBody] PatchDestinationDto dto)
    {
        var result = await _service.UpdateForTrip(tripId,id,dto);
        return Ok(result);
    }
    
    [HttpPost("/trip/{tripId}/day/{dayId}")]
    public async Task<IActionResult> CreateForDay(string tripId,string dayId, [FromBody] CreateDestinationDto dto)
    {
        await _service.CreateForDay(tripId,dayId, dto);
        return Created();
    }
    [HttpDelete("/trip/{tripId}/day/{dayId}/destination/{id}")]
    public async Task<IActionResult> DeleteForDay(string tripId,string dayId,string id, [FromBody] CreateDestinationDto dto)
    {
        await _service.DeleteForDay(tripId,dayId,id,dto);
        return NoContent();
    }
    [HttpPatch("/trip/{tripId}/day/{dayId}/destination/{id}")]
    public async Task<IActionResult> UpdateForDay(string tripId,string dayId,string id, [FromBody] PatchDestinationDto dto)
    {
        await _service.UpdateForDay(tripId,dayId,id,dto);
        return NoContent();
    }
}
