using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinkController : BaseApiController
{
    private readonly LinkService _service;

    public LinkController(LinkService service)
    {
        _service = service;
    }

    [HttpPost("/trip/{tripId}")]
    public async Task<IActionResult> CreateForTrip(string tripId, [FromBody] CreateLinkDto dto)
    {
        var result = await _service.CreateForTrip(tripId, dto);
        return Created();
    }

    [HttpDelete("/trip/{tripId}/link/{id}")]
    public async Task<IActionResult> DeleteForTrip(string tripId,string id)
    {
        var result = await _service.DeleteForTrip(tripId, id);
        
        return NoContent();
    }

    [HttpPatch("/trip/{tripId}/link/{id}")]
    public async Task<IActionResult> UpdateForTrip(string tripId, string id, [FromBody] PatchLinkDto dto)
    {
        var result = await _service.UpdateForTrip(tripId, id, dto);
        return Ok(result);
    }
}