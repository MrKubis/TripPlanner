using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Domain.Specifications;
using backend.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly IMongoCollection<Trip> _trips;
    
    public TripRepository(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }
    
    public async Task<Pagination<Trip>> GetAllAsync(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Trip>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(catalogSpecParams.Search))
        {
            filter &= builder.Where(t => t.Title.ToLower()
                .Contains(catalogSpecParams.Search.ToLower()));
        }
        var totalItems = await _trips.CountDocumentsAsync(filter);
        var data = await ApplyDataFilters(filter, catalogSpecParams);

        return new Pagination<Trip>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            (int)totalItems,
            data);
    }

    private async Task<IReadOnlyCollection<Trip>> ApplyDataFilters(FilterDefinition<Trip> filter,
        CatalogSpecParams catalogSpecParams)
    {
        var sortDefinition = Builders<Trip>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            sortDefinition = catalogSpecParams.Sort switch
            {
                "titleAsc" => Builders<Trip>.Sort.Ascending(t => t.Title),
                "titleDesc" => Builders<Trip>.Sort.Descending(t => t.Title),
                _ => Builders<Trip>.Sort.Ascending(t => t.Title)
            };
        }
        return await _trips
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
    }

    public async Task<Trip?> GetByIdAsync(string tripId)
    {
        var filter =  Builders<Trip>.Filter.Eq(x => x.Id,tripId);
        var trip = await _trips.Find(filter).FirstOrDefaultAsync();
        return trip;
    }

    public async Task<Trip> CreateAsync(Trip trip)
    {    
        await _trips.InsertOneAsync(trip);
        return trip;
    }

    public async Task<bool> DeleteAsync(string tripId)
    {
        var deletedTrip = await _trips.DeleteOneAsync(t => t.Id == tripId);
        return deletedTrip.IsAcknowledged && deletedTrip.DeletedCount > 0;
    }

    public async Task<bool> UpdateAsync(string tripId, Trip trip)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, tripId);
        var update = Builders<Trip>.Update
            .Set(x => x.Title, trip.Title)
            .Set(x => x.Description, trip.Description);
        var result = await _trips.UpdateOneAsync(filter, update);
        return result.IsAcknowledged && result.MatchedCount > 0;
    }
}