using backend.Domain.Entities;
using MongoDB.Driver;

namespace backend.Domain.Repositories;

public interface IDayRepository
{
    Task<DayRepositoryResult> CreateForTrip(string tripId,Day day);
    Task<DayRepositoryResult> DeleteForTrip(string tripId,string dayId);
    Task<DayRepositoryResult> UpdateForTrip(string tripId, Day day);
}

public enum DayRepositoryResult
{
    Success,
    DayNotFound,
    TripNotFound
}