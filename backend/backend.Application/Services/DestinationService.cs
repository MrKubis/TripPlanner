using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class DestinationService(IDestinationRepository destinationRepository) : IDestinationService
{
    public async Task<Result<DestinationDto>> CreateForTripAsync(Guid tripId, CreateDestinationDto dto)
    {
        var destination = dto.ToEntity();
        var result = await destinationRepository.CreateForTripAsync(tripId, destination);
        if (result.IsFailure)
        {
            return Result<DestinationDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }
        
        return Result<DestinationDto>.Success(destination.ToDto());
    }

    public async Task<Result<DestinationDto>> CreateForDayAsync(Guid dayId, CreateDestinationDto dto)
    {
        var destination = dto.ToEntity();
        var result = await destinationRepository.CreateForDayAsync(dayId, destination);
        if (result.IsFailure)
        {
            return Result<DestinationDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }
        
        return Result<DestinationDto>.Success(destination.ToDto());
    }

    public async Task<Result<DestinationDto>> UpdateAsync(Guid destinationId, UpdateDestinationDto dto)
    {
        var destination = dto.ToEntity(destinationId);
        var result = await destinationRepository.UpdateAsync(destinationId, destination);
        if (result.IsFailure)
        {
            return Result<DestinationDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }

        return Result<DestinationDto>.Success(destination.ToDto());
    }

    public Task<Result> DeleteAsync(Guid destinationId)
    {
        var result = destinationRepository.DeleteAsync(destinationId);
        return result;
    }
}
