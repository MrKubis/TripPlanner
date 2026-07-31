using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Domain.Specifications;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class TripRepository(AppDbContext context) : ITripRepository
{
    public async Task<ICollection<Trip>> GetPaginationAsync(CatalogSpecParams catalogSpecParams)
    {
        return await context.Trips
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Take(catalogSpecParams.PageSize)
            .ToListAsync();
    }

    public async Task<Trip?> GetByIdAsync(Guid tripId)
    {
        return await context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
    }

    public async Task<Trip> CreateAsync(Trip trip)
    {    
        await context.Trips.AddAsync(trip);
        return trip;
    }

    public async Task<bool> DeleteAsync(Guid tripId)
    {
        await context.Trips
            .Where(t => t.Id == tripId)
            .ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Trip trip)
    {
        context.Trips.Update(trip);
        await context.SaveChangesAsync();
        return true;
    }
}