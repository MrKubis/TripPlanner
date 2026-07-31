using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class LinkService(ILinkRepository linkRepository) : ILinkService
{
    public async Task<Result<LinkDto>> CreateForTripAsync(Guid tripId, CreateLinkDto dto)
    {
        var link = dto.ToEntity();
        var result = await linkRepository.CreateForTripAsync(tripId, link);
        if (result.IsFailure)
        {
            return Result<LinkDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }
        
        return Result<LinkDto>.Success(link.ToDto());
    }

    public async Task<Result> DeleteAsync(Guid linkId)
    {
        var result = await linkRepository.DeleteAsync(linkId);
        return result;
    }

    public async Task<Result<LinkDto>> UpdateAsync(Guid linkId, UpdateLinkDto dto)
    {
        var link = dto.ToEntity(linkId);
        var result = await linkRepository.UpdateAsync(linkId, link);
        if (result.IsFailure)
        {
            return Result<LinkDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }
        
        return Result<LinkDto>.Success(link.ToDto());
    }
}