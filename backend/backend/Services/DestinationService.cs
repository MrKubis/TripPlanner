using backend.Domain.Entities;
using backend.Dtos;
using backend.Exceptions.Exceptions;
using MongoDB.Bson;
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
                LinkIds = new List<string>()
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
        var newDestination = new Destination
        {
            Name = dto.Name,
            Location = new Location
            {
                Longitude = dto.Location.Longitude,
                Latitude = dto.Location.Latitude
            }
        };
        
        var filter = Builders<Trip>.Filter.Eq(x => x.Id,tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Days,day => day.Id == dayId);
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.Push(t => t.Destinations, newDestination),
            Builders<Trip>.Update.Set("days.$[day].destinationIds", newDestination.Id)
            );
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("day._id", ObjectId.Parse(dayId)))
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task DeleteForDay(string tripId, string dayId, string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Days, day => day.Id == dayId);
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.PullFilter(t=>t.Destinations, destination => destination.Id == id),
            Builders<Trip>.Update.Pull("days.$[day].destinationIds",id));
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("day._id", ObjectId.Parse(dayId)))
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }
}
