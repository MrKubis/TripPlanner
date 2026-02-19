using backend.Domain.Entities;
using backend.Dtos;
using backend.Exceptions.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace backend.Services;

public class TripService
{
    private readonly IMongoCollection<Trip> _trips;

    public TripService(IMongoCollection<Trip> trips)
    {
        _trips = trips;
    }
    
    public async Task<IEnumerable<TripDto>> GetAllAsync()
    {
        var trips = await _trips.Find(_ => true).ToListAsync();
        return trips.Select(trip => new TripDto
        {
            Id = trip.Id,
            CreatedBy = trip.CreatedBy,
            CreatedOn = trip.CreatedOn,
            Title = trip.Title,
        }).ToList();
    }

    public async Task<Trip> GetByIdAsync(string id)
    {
        var filter =  Builders<Trip>.Filter.Eq(x => x.Id,id);
        var trip = await _trips.Find(filter).FirstOrDefaultAsync();
        if (trip == null)
        {
            throw new NotFoundException(id);
        }
        return trip;
    }

    public async Task<Trip> Create(CreateTripDto dto)
    {
        var trip = new Trip
        {
            Title = dto.Title,
            Links = new List<Link>(),
            Days = new List<Day>(),
            Destinations = new List<Destination>(),
            Expenses = new List<Expense>(),
            CreatedOn = DateTime.UtcNow,
            CreatedBy = null
                
        };
        await _trips.InsertOneAsync(trip);

        return trip;
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Trip>.Filter.Eq(x => x.Id, id);
        await _trips.DeleteOneAsync(filter);
    }
    
}