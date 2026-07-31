using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class DestinationRepository (AppDbContext context): IDestinationRepository
{
    public async Task<Result> CreateForTripAsync(Guid tripId, Destination destination)
    {
        var tripExists = await context.Trips.AnyAsync(x => x.Id == tripId);
        if (!tripExists)
        {
            return Result.Failure("Trip not found", ErrorType.NotFound);
        }

        destination.TripId = tripId;
        context.Destinations.Add(destination);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid destinationId)
    {
        await context.Destinations
            .Where(x => x.Id == destinationId)
            .ExecuteDeleteAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid destinationId, Destination destination)
    {
        var existingDestination = await context.Destinations.FirstOrDefaultAsync(x => x.Id == destinationId);
        if (existingDestination == null)
        {
            return Result.Failure("Destination not found", ErrorType.NotFound);
        }

        existingDestination.Name = destination.Name;
        existingDestination.Location = destination.Location;
        return Result.Success();
    }

    public async Task<Result> CreateForDayAsync(Guid dayId, Destination destination)
    {
        var day = await context.Days.FirstOrDefaultAsync(x => x.Id == dayId);
        if (day == null)
        {
            return Result.Failure("Day not found, ErrorType.NotFound");
        }
        
        destination.TripId = day.TripId;
        destination.DayId = dayId;
        await context.SaveChangesAsync();
        return Result.Success();
    }
}