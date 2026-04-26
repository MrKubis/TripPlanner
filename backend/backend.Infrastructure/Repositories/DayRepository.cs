using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Infrastructure.Repositories;

public class DayRepository : IDayRepository
{
    private readonly IMongoCollection<Trip> _trips;

    public DayRepository(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }
    
    public async Task<DayRepositoryResult> CreateForTrip(string tripId, Day day)
    {
        var filter =  Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .PushEach(
                trip => trip.Days,
                [day],
                sort: Builders<Day>.Sort.Ascending(d=>d.Date)
                );
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return DayRepositoryResult.TripNotFound;
        return DayRepositoryResult.Success; 
    }

    public async Task<DayRepositoryResult> DeleteForTrip(string tripId, string dayId)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .PullFilter(trip => trip.Days, day => day.Id == dayId);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return DayRepositoryResult.TripNotFound;
        if (result.ModifiedCount == 0) return DayRepositoryResult.DayNotFound;
        return DayRepositoryResult.Success; 
    }

    public async Task<DayRepositoryResult> UpdateForTrip(string tripId, Day day)
    {
        var tripExists = await _trips.Find(x => x.Id == tripId).AnyAsync();
        if(!tripExists) return DayRepositoryResult.TripNotFound;
        
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x=>x.Days, tDay => tDay.Id == day.Id);
        var update = Builders<Trip>.Update.Set("Days.$", day);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return DayRepositoryResult.DayNotFound;
        return DayRepositoryResult.Success;  
    }
}