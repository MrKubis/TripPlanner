using backend.API.Extensions;
using backend.Application.DTOs;
using backend.Application.Services;
using backend.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1/trips")]
public class TripController(ITripService tripService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetPaginationAsync(
        [FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var result = await tripService.GetPaginationAsync(catalogSpecParams);
        return Ok(result);
    }

    [HttpGet("{tripId}")]
    public async Task<ActionResult<TripDetailsDto>> GetByIdAsync(Guid tripId)
    {
        var result = await tripService.GetByIdAsync(tripId);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<TripDetailsDto>> PostAsync([FromBody] CreateTripDto dto)
    {
        var result = await tripService.Create(dto);
        return result.ToActionResult();
    }

    [HttpDelete("{tripId}")]
    public async Task<IActionResult> DeleteAsync(Guid tripId)
    {
        var result = await tripService.Delete(tripId);
        return result.ToActionResult();
    }

    [HttpPatch("{tripId}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid tripId, [FromBody] UpdateTripDto dto)
    {
        var result = await tripService.Update(tripId,dto);
        return result.ToActionResult();
    }
}