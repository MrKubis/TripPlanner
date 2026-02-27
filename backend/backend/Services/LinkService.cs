using backend.Domain.Entities;
using backend.Dtos;
using backend.Exceptions.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Services;

public class LinkService
{
    private readonly IMongoCollection<Trip> _trips;

    public LinkService(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }

    public async Task CreateForTrip(string tripId, CreateLinkDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);

        var update = Builders<Trip>.Update
            .Push(trip => trip.Links, new Link
            {
                Title = dto.Title,
                Url = dto.Url
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
            .PullFilter(trip => trip.Links, link => link.Id == id);

        var result = await _trips.UpdateOneAsync(filter, pullUpdate);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task<UpdateResult> UpdateForTrip(string tripId, string id, PatchLinkDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x=>x.Links, link => link.Id == id);
        var updateDef = new List<UpdateDefinition<Trip>>();
        if (dto.Url != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("links.$.url", dto.Url));
        }
        if (dto.Title != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("links.$.title", dto.Title));
        }

        var update = Builders<Trip>.Update.Combine(updateDef);
        var result = await _trips.UpdateOneAsync(filter,update);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
        return result;
    }

    public async Task CreateForDestination(string tripId, string destinationId, CreateLinkDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations, destination => destination.Id == destinationId);
        var update = Builders<Trip>.Update
            .Push("destinations.$[dest].links", new Link
            {
                Title = dto.Title,
                Url = dto.Url
            });
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("dest._id", ObjectId.Parse(destinationId)))
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task DeleteForDestination(string tripId, string destinationId, string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.ElemMatch(x => x.Destinations,
                         destination => destination.Id == destinationId);
        var update = Builders<Trip>.Update
            .PullFilter(
                new StringFieldDefinition<Trip, List<Link>>("destinations.$[dest].links"),
                Builders<Link>.Filter.Eq(x => x.Id, id));
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("dest._id", ObjectId.Parse(destinationId)))
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }

    public async Task UpdateForDestination(string tripId, string destinationId, string id, PatchLinkDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var updateDef = new List<UpdateDefinition<Trip>>();
        if (dto.Url != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("destinations.$[dest].links.$[link].url",dto.Url));
        }

        if (dto.Title != null)
        {
            updateDef.Add(Builders<Trip>.Update.Set("destinations.$[dest].links.$[link].title", dto.Title));
        }
        var update = Builders<Trip>.Update.Combine(updateDef);
        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("dest._id", ObjectId.Parse(destinationId))),
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("link._id", ObjectId.Parse(id))),
        };
        var options = new UpdateOptions { ArrayFilters = arrayFilters };
        var result = await _trips.UpdateOneAsync(filter, update, options);
        if (result.MatchedCount == 0)
        {
            throw new NotFoundException(tripId);
        }
    }
}