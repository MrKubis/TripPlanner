using backend.Application.DTOs;
using backend.Application.Mappers;
using backend.Domain.Common.Results;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class DayService(IDayRepository dayRepository) : IDayService
{
    public async Task<Result<DayDto>> CreateForTripAsync(Guid tripId, CreateDayDto dto)
    {
        var day = dto.ToEntity();
        var result = await dayRepository.CreateForTripAsync(tripId, day);
        if (result.IsFailure)
        {
            return Result<DayDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }

        return Result<DayDto>.Success(day.ToDto());
    }

    public async Task<Result<DayDto>> UpdateAsync(Guid dayId, UpdateDayDto dto)
    {
        var day = dto.ToEntity(dayId);
        var result = await dayRepository.UpdateAsync(day);
        if (result.IsFailure)
        {
            return Result<DayDto>.Failure(
                result.Error ?? "",
                result.ErrorType ?? ErrorType.Generic);
        }
        return Result<DayDto>.Success(day.ToDto());
    }

    public async Task<Result> DeleteAsync(Guid dayId)
    {
        var result = await dayRepository.DeleteAsync(dayId);
        return result;
    }

    public async Task<Result> AppendDestinationAsync(Guid dayId, Guid destinationId)
    {
        var result = await dayRepository.AppendDestinationAsync(dayId, destinationId);
        return result;
    }
}