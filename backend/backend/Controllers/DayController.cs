using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DayController : BaseApiController
{
    private readonly DayService _service;

    public DayController(DayService service)
    {
        _service = service;
    }

    [HttpPost("/trip/{tripId}")]
    public async Task<IActionResult> CreateForTrip(string tripId, [FromBody] CreateDayDto dto)
    {
        await _service.CreateForTrip(tripId, dto);
        return Created();
    }

    [HttpDelete("/trip/{tripId}/day/{id}")]
    public async Task<IActionResult> DeleteForTrip(string tripId,string id)
    {
        await _service.DeleteForTrip(tripId, id);
        return NoContent();
    }

    [HttpPatch("/trip/{tripId}/day/{id}")]
    public async Task<IActionResult> UpdateForTrip(string tripId, string id, [FromBody] PatchDayDto dto)
    {
        var result = await _service.UpdateForTrip(tripId, id, dto);
        return Ok(result);
    }
}