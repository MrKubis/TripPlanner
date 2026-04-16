using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

public class LinkController : ControllerBase
{
    private readonly LinkService _service;

    public LinkController(LinkService service)
    {
        _service = service;
    }

    [HttpPost("/trip/{tripId}/link")]
    public async Task<IActionResult> CreateForTrip(string tripId, [FromBody] CreateLinkDto dto)
    { 
        await _service.CreateForTrip(tripId, dto);
        return Created();
    }

    [HttpDelete("/trip/{tripId}/link/{id}")]
    public async Task<IActionResult> DeleteForTrip(string tripId,string id)
    {
        await _service.DeleteForTrip(tripId, id);
        return NoContent();
    }

    [HttpPatch("/trip/{tripId}/link/{id}")]
    public async Task<IActionResult> UpdateForTrip(string tripId, string id, [FromBody] UpdateLinkDto dto)
    {
        var result = await _service.UpdateForTrip(tripId, id, dto);
        return Ok(result);
    }

    [HttpPatch("/trip/{tripId}/destination/{destinationId}/append/link/{id}")]
    public async Task<IActionResult> AppendLinkToDestination(string tripId, string destinationId, string id)
    {
        await _service.AppendLinkToDestination(tripId, destinationId, id);
        return Ok();
    }

    [HttpPatch("/trip/{tripId}/destination/{destinationId}/remove/link/{id}")]
    public async Task<IActionResult> RemoveLinkToDestination(string tripId, string destinationId, string id)
    {
        await _service.RemoveLinkFromDestination(tripId, destinationId, id);
        return Ok();
    }
}