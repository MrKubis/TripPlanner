using backend.Application.DTOs;
using backend.Domain.Common.Results;

namespace backend.Application.Services;

public interface IDayService
{
    Task<Result<DayDto>> CreateForTripAsync(Guid tripId, CreateDayDto dto);
    Task<Result<DayDto>> UpdateAsync(Guid dayId, UpdateDayDto dto);
    Task<Result> DeleteAsync(Guid dayId);
    Task<Result> AppendDestinationAsync(Guid dayId, Guid destinationId);
}