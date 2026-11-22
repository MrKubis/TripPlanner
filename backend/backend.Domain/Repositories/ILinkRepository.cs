using backend.Domain.Common.Results;
using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface ILinkRepository
{
    Task<Result> CreateForTripAsync(Guid tripId, Link link);
    Task<Result> UpdateAsync(Guid linkId, Link link);
    Task<Result> DeleteAsync(Guid linkId);
}
