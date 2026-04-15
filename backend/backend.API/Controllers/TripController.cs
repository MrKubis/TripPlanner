using backend.Application.DTOs;
using backend.Application.Services;
using backend.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/v1/trip")]
public class TripController : ControllerBase
{
    private readonly TripService _tripService;

    public TripController(TripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetAllAsync([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var result = await _tripService.GetAllAsync(catalogSpecParams);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> GetByIdAsync(string id)
    {
        var result = await _tripService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TripDto>> PostAsync([FromBody] CreateTripDto dto)
    {
        var result = await _tripService.Create(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        await _tripService.Delete(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateTripDto dto)
    {
        await _tripService.Update(id,dto);
        return NoContent();
    }
}