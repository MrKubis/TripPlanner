using backend.API.Extensions;
using backend.Application.DTOs;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

public class LinkController(ILinkService linkService) : ControllerBase
{
    [HttpPost("/trips/{tripId}/links")]
    public async Task<ActionResult<LinkDto>> CreateLinkForTrip(Guid tripId, [FromBody] CreateLinkDto dto)
    { 
        var result = await linkService.CreateForTripAsync(tripId, dto);
        return result.ToActionResult();
    }

    [HttpDelete("links/{linkId}")]
    public async Task<IActionResult> DeleteLink(Guid linkId) 
    {
        var result = await linkService.DeleteAsync(linkId);
        return result.ToActionResult();
    }

    [HttpPatch("links/{linkId}")]
    public async Task<IActionResult> UpdateLink(Guid linkId, [FromBody] UpdateLinkDto dto)
    {
        var result = await linkService.UpdateAsync(linkId, dto);
        return Ok(result);
    }
}