using backend.Application.DTOs;
using backend.Domain.Common.Results;

namespace backend.Application.Services;

public interface IDestinationService
{
    public Task<Result<DestinationDto>> CreateForTripAsync(Guid tripId, CreateDestinationDto dto);
    public Task<Result<DestinationDto>> CreateForDayAsync(Guid dayId, CreateDestinationDto dto);
    public Task<Result<DestinationDto>> UpdateAsync(Guid destinationId, UpdateDestinationDto dto);
    public Task<Result> DeleteAsync(Guid destinationId);
}