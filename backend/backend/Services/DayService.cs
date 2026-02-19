using backend.Domain.Entities;
using backend.Dtos;
using backend.Exceptions.Exceptions;
using MongoDB.Driver;

namespace backend.Services;

public class DayService
{
    private readonly IMongoCollection<Trip> _trips;

    public DayService(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }

    public async Task CreateForTrip(string tripId, CreateDayDto dto)
    {
        var filter =  Builders<Trip>.Filter.Eq(x => x.Id, tripId);

        var update = Builders<Trip>.Update
            .Push(trip => trip.Days, new Day
            {
                Date = dto.Date
            });
        var result = await _trips.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task DeleteForTrip(string tripId, string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var pullUpdate = Builders<Trip>.Update
            .PullFilter(x => x.Days, day => day.Id == id);
        var result = await _trips.UpdateOneAsync(filter, pullUpdate);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task<object?> UpdateForTrip(string tripId, string id, PatchDayDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x=>x.Days, day => day.Id == id);
        var updateDef = new List<UpdateDefinition<Trip>>();

        if (dto.Date != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("days.$.date",dto.Date));
        }

        var update = Builders<Trip>.Update.Combine(updateDef);
        var result = await _trips.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
        return result;
    }
}