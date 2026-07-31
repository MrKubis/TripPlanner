using backend.Application.DTOs;
using backend.Domain.Common.Results;

namespace backend.Application.Services;

public interface ILinkService
{
    public Task<Result<LinkDto>> CreateForTripAsync(Guid tripId, CreateLinkDto dto);
    public Task<Result> DeleteAsync(Guid linkId);
    public Task<Result<LinkDto>> UpdateAsync(Guid linkId, UpdateLinkDto dto);
}