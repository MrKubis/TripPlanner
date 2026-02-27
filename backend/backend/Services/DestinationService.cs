using backend.Domain.Entities;
using backend.Dtos;
using backend.Exceptions.Exceptions;
using MongoDB.Driver;

namespace backend.Services;

public class DestinationService
{
    private readonly IMongoCollection<Trip> _trips;

    public DestinationService(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }

    public async Task CreateForTrip(string tripId, CreateDestinationDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);

        var update = Builders<Trip>.Update
            .Push(trip => trip.Destinations, new Destination
            {
                Location = new Location
                {
                    Longitude = dto.Location.Longitude,
                    Latitude = dto.Location.Latitude
                },
                Name = dto.Name,
                Links = new List<Link>()
            });
        var result = await _trips.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task DeleteForTrip(string tripId,string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var pullUpdate = Builders<Trip>.Update
            .PullFilter(trip => trip.Destinations, destination => destination.Id == id);
        var result = await _trips.UpdateOneAsync(filter,pullUpdate);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task<UpdateResult> UpdateForTrip(string tripId, string id, PatchDestinationDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id,tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination => destination.Id == id);
        var updateDef = new List<UpdateDefinition<Trip>>();
        if (dto.Name != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("destinations.$.name",dto.Name));
        }

        if (dto.Location != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("destinations.$.location",dto.Location));
        }
        var update = Builders<Trip>.Update.Combine(updateDef);
        var result = await _trips.UpdateOneAsync(filter,update);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
        return result;
    }

    public async Task CreateForDay(string tripId, string dayId, CreateDestinationDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteForDay(string tripId, string dayId, string id, CreateDestinationDto dto)
    {
        throw new NotImplementedException();
    }
    
    public async Task UpdateForDay(string tripId, string id, PatchDestinationDto dto)
    {
        throw new NotImplementedException();
    }
}
