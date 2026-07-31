using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class LinkRepository(AppDbContext context) : ILinkRepository
{
    public async Task<Result> CreateForTripAsync(Guid tripId, Link link)
    {
        var tripExists = await context.Trips.AnyAsync(x => x.Id == tripId);
        if (!tripExists)
        {
            return Result.Failure("Trip not found", ErrorType.NotFound);
        }
        
        link.TripId = tripId;
        context.Links.Add(link);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid linkId, Link link)
    {
        var existingLink = await context.Links.FirstOrDefaultAsync(x => x.Id == linkId);
        if (existingLink == null)
        {
            return Result.Failure("Link not found", ErrorType.NotFound);
        }

        existingLink.Url = link.Url;
        existingLink.Title = link.Title;
        await context.SaveChangesAsync();
        
        return Result.Success(); 
    }

    public async Task<Result> DeleteAsync(Guid linkId)
    {
        await context.Links
            .Where(x => x.Id == linkId)
            .ExecuteDeleteAsync();
        
        return Result.Success();
    }
}