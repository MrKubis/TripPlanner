using backend.Domain.Common.Results;
using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IDestinationRepository
{
    Task<Result> CreateForTripAsync(Guid tripId, Destination destination);
    Task<Result> DeleteAsync(Guid destinationId);
    Task<Result> UpdateAsync(Guid destinationId, Destination destination);
    Task<Result> CreateForDayAsync(Guid dayId, Destination destination);
}