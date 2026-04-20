using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IDestinationRepository
{
    Task<DestinationRepositoryResult> CreateForTrip(string tripId,Destination destination);
    Task<DestinationRepositoryResult> DeleteForTrip(string tripId, string destinationId);
    Task<DestinationRepositoryResult> UpdateForTrip(string tripId, Destination destination);
    Task<DestinationRepositoryResult> CreateForDay(string tripId,string dayId,Destination destination);   
    Task<DestinationRepositoryResult> DeleteForDay(string tripId, string dayId,string id);
    Task<DestinationRepositoryResult> AppendToDay(string tripId, string dayId, string id);

    Task<DestinationRepositoryResult> RemoveFromDay(string tripId, string dayId, string id);
}

public enum DestinationRepositoryResult
{
    Success,
    DestinationNotFound,
    TripNotFound,
    DayNotFound
}