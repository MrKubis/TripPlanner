using backend.Domain.Common.Results;
using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IDayRepository
{
    Task<Result> CreateForTripAsync(Guid tripId, Day day);
    Task<Result> DeleteAsync(Guid dayId);
    Task<Result> UpdateAsync(Day day);
    Task<Result> AppendDestinationAsync(Guid dayId, Guid destinationId);
}
