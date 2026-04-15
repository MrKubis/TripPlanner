using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Infrastructure.Repositories;

public class LinkRepository : ILinkRepository
{
    
    private readonly IMongoCollection<Trip> _trips;

    public LinkRepository(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    } 

    public async Task<LinkRepositoryResult> CreateForTrip(string tripId, Link link)
    {
        var filter = Builders<Trip>.Filter.Eq(t => t.Id, tripId);
        var update = Builders<Trip>.Update
            .Push(trip => trip.Links, link);
        var result = await _trips.UpdateOneAsync(filter, update);
        if (result.MatchedCount == 0) return LinkRepositoryResult.TripNotFound;
        return LinkRepositoryResult.Success;
    }

    public async Task<LinkRepositoryResult> UpdateForTrip(string tripId, Link link)
    {
        var tripExists = await _trips.Find(t => t.Id == tripId).AnyAsync();
        if (!tripExists) return LinkRepositoryResult.TripNotFound;
        
        var filter = Builders<Trip>.Filter.Eq(t => t.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(t => t.Links, tripLink => tripLink.Id == link.Id);
        var update = Builders<Trip>.Update.Set("Links.$", link);
        
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return LinkRepositoryResult.LinkNotFound;
        return LinkRepositoryResult.Success;
    }

    public async Task<LinkRepositoryResult> DeleteForTrip(string tripId, string linkId)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .PullFilter(trip => trip.Links, link=> link.Id == linkId);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return LinkRepositoryResult.TripNotFound;
        if (result.ModifiedCount == 0) return LinkRepositoryResult.LinkNotFound;
        return LinkRepositoryResult.Success; 
    }

    public async Task<LinkRepositoryResult> CreateForDestination(string tripId, string destinationId, Link link)
    {
        var tripExists = await _trips.Find(t => t.Id == tripId).AnyAsync();
        if (!tripExists) return LinkRepositoryResult.TripNotFound;
        
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination => destination.Id == destinationId);
        
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.Push(t => t.Links, link),
            Builders<Trip>.Update.Push("destinations.$[dest].linkIds", link.Id));
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("dest._id", ObjectId.Parse(destinationId)))
        };
        
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0) return LinkRepositoryResult.DestinationNotFound;
        return LinkRepositoryResult.Success;
    }

    public async Task<LinkRepositoryResult> DeleteForDestination(string tripId, string destinationId, string id)
    {
        var trip = await _trips.Find(x => x.Id == tripId).FirstOrDefaultAsync();
        if (trip is null)
            return LinkRepositoryResult.TripNotFound;

        var dayExists = trip.Days.Any(d => d.Id == destinationId);
        if (!dayExists)
            return LinkRepositoryResult.DestinationNotFound;
        
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Links, link=> link.Id == id);
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.PullFilter(t=>t.Destinations, destination => destination.Id == destinationId),
            Builders<Trip>.Update.Pull("destinations.$[dest].linkIds",id));
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("dest._id", ObjectId.Parse(destinationId))),
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            return LinkRepositoryResult.DestinationNotFound;
        }
        return LinkRepositoryResult.Success;
    }
}