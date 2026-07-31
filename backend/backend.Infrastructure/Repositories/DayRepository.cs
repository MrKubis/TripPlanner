using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class DayRepository(AppDbContext context) : IDayRepository
{
    public async Task<Result> CreateForTripAsync(Guid tripId, Day day)
    {
        var tripExists = await context.Trips.AnyAsync(x => x.Id == tripId);
        if (!tripExists)
        {
            return Result.Failure("Trip not found", ErrorType.NotFound);
        }
        day.TripId = tripId;
        context.Days.Add(day);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid dayId)
    {
        await context.Days
            .Where(x => x.Id == dayId)
            .ExecuteDeleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Day day)
    {
        var existingDay = await context.Days.FirstOrDefaultAsync(x => x.Id == day.Id);
        if (existingDay == null)
        {
            return Result.Failure("Day not found", ErrorType.NotFound);
        }

        existingDay.Date = day.Date;
        return Result.Success();
    }

    public async Task<Result> AppendDestinationAsync(Guid dayId, Guid destinationId)
    {
        var destination = await context.Destinations
            .FirstOrDefaultAsync(x => x.Id == destinationId);
        if (destination == null)
        {
            return Result.Failure("Destination not found", ErrorType.NotFound);
        }
        
        var day = await context.Days.FirstOrDefaultAsync(x => x.Id == dayId);
        if (day == null)
        {
            return Result.Failure("Day not found", ErrorType.NotFound);
        }
        day.Destinations.Add(destination);
        await context.SaveChangesAsync();
        return Result.Success();
    }
}