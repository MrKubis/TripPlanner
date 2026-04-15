using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface ILinkRepository
{
    Task<LinkRepositoryResult> CreateForTrip(string tripId, Link link);
    Task<LinkRepositoryResult> UpdateForTrip(string tripId, Link link);
    Task<LinkRepositoryResult> DeleteForTrip(string tripId, string linkId);
    Task<LinkRepositoryResult> CreateForDestination(string tripId, string destinationId, Link link);
    Task<LinkRepositoryResult> DeleteForDestination(string tripId, string destinationId, string id);
}

public enum LinkRepositoryResult
{
    Success,
    TripNotFound,
    LinkNotFound,
    DestinationNotFound,
}