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

    public async Task<UpdateResult> CreateForTrip(string tripId, CreateLinkDto dto)
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
        return result;
    }

    public async Task<UpdateResult> DeleteForTrip(string tripId, string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);

        var pullUpdate = Builders<Trip>.Update
            .PullFilter(trip => trip.Links, link => link.Id == id);

        var result = await _trips.UpdateOneAsync(filter, pullUpdate);
        return result;
    }

    public async Task<UpdateResult> UpdateForTrip(string tripId, string id, PatchLinkDto dto)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId) &
                     Builders<Trip>.Filter.Eq("links._id",id);
        var update = Builders<Trip>.Update
            .Set("links.$.url", dto.Url);

        if (dto.Title != null)
        {
            update.Set("links.$.title", dto.Title);
        }
        var result = await _trips.UpdateOneAsync(filter,update);
        return result;
    }
}