using backend.Domain.Entities;
using backend.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Infrastructure.Repositories;

public class DestinationRepository: IDestinationRepository
{
    private readonly IMongoCollection<Trip> _trips;

    public DestinationRepository(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }

    public async Task<DestinationRepositoryResult> CreateForTrip(string tripId, Destination destination)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .Push(trip => trip.Destinations, destination);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return DestinationRepositoryResult.TripNotFound;
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> DeleteForTrip(string tripId, string destinationId)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .PullFilter(x => x.Destinations,destination => destination.Id == destinationId);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if (result.MatchedCount == 0) return DestinationRepositoryResult.TripNotFound;
        if (result.ModifiedCount == 0) return DestinationRepositoryResult.DestinationNotFound;
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> UpdateForTrip(string tripId, Destination destination)
    {
        var tripExists = await _trips.Find(x => x.Id == tripId).AnyAsync();
        if (!tripExists)
        {
            return DestinationRepositoryResult.TripNotFound;
        }
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) & 
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations, tripDestination => tripDestination.Id == destination.Id);
        var update = Builders<Trip>.Update.Set("Destinations.$", destination);
        var result = await _trips.UpdateOneAsync(filter, update);
        
        if(result.MatchedCount == 0) return DestinationRepositoryResult.DestinationNotFound;
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> CreateForDay(string tripId, string dayId, Destination destination)
    {
        var tripExists = await _trips.Find(x => x.Id == tripId).AnyAsync();
        if (!tripExists)
        {
            return DestinationRepositoryResult.TripNotFound;
        }
        var filter = Builders<Trip>.Filter.Eq(x => x.Id,tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Days,day => day.Id == dayId);
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.Push(trip => trip.Destinations, destination),
            Builders<Trip>.Update.Set("days.$[day].destinationIds", destination.Id));
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("day._id", ObjectId.Parse(dayId)))
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if(result.MatchedCount == 0) return DestinationRepositoryResult.DayNotFound;
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> DeleteForDay(string tripId, string dayId,string id)
    {
        var trip = await _trips.Find(x => x.Id == tripId).FirstOrDefaultAsync();
        if (trip is null)
            return DestinationRepositoryResult.TripNotFound;

        var dayExists = trip.Days.Any(d => d.Id == dayId);
        if (!dayExists)
            return DestinationRepositoryResult.DayNotFound;
        
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination=> destination.Id == id);
        var update = Builders<Trip>.Update.Combine(
            Builders<Trip>.Update.PullFilter(t=>t.Days, day=> day.Id == dayId),
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
            return DestinationRepositoryResult.DestinationNotFound;
        }
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> AppendToDay(string tripId, string dayId, string id)
    {
        var trip =  await _trips.Find(x => x.Id == tripId).FirstOrDefaultAsync();
        if (trip == null)
        {
            return DestinationRepositoryResult.TripNotFound;
        }
        var destinationExists = trip.Destinations.Any(d => d.Id == id);
        if (!destinationExists)
        {
            return DestinationRepositoryResult.DestinationNotFound;
        }
        var filter =  Builders<Trip>.Filter.Eq(t => t.Id, tripId) &
                      Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination=> destination.Id == id);
        
        var update = Builders<Trip>.Update.Push("days.$[day].destinationIds", id);
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("day._id", ObjectId.Parse(dayId))),
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);

        if(result.MatchedCount == 0) return DestinationRepositoryResult.DayNotFound;
        
        return DestinationRepositoryResult.Success;
    }

    public async Task<DestinationRepositoryResult> RemoveFromDay(string tripId, string dayId, string id)
    {
        var trip =  await _trips.Find(x => x.Id == tripId).FirstOrDefaultAsync();
        if (trip == null)
        {
            return DestinationRepositoryResult.TripNotFound;
        }
        var destinationExists = trip.Destinations.Any(d => d.Id == id);
        if (!destinationExists)
        {
            return DestinationRepositoryResult.DestinationNotFound;
        }
        var filter =  Builders<Trip>.Filter.Eq(t => t.Id, tripId) &
                      Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination=> destination.Id == id);
        
        var update = Builders<Trip>.Update.Pull("days.$[day].destinationIds", id);
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("day._id", ObjectId.Parse(dayId))),
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);

        if(result.MatchedCount == 0) return DestinationRepositoryResult.DayNotFound;
        
        return DestinationRepositoryResult.Success;
    }
}