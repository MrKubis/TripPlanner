using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class DayService
{

    private readonly IDayRepository _dayRepository;
    
    public DayService(IDayRepository dayRepository)
    {
        _dayRepository = dayRepository;
    }

    public async Task<DayDto> CreateForTrip(string tripId, CreateDayDto dto)
    {
        var day =  dto.ToEntity();
        var result = await _dayRepository.CreateForTrip(tripId, day);
        
        return result switch
        {
            DayRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip),tripId),
            DayRepositoryResult.Success => day.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }

    public async Task DeleteForTrip(string tripId, string id)
    {
        var result = await _dayRepository.DeleteForTrip(tripId, id);
        
        if (result ==  DayRepositoryResult.TripNotFound)
            throw new NotFoundException(typeof(Trip), id);
        if(result == DayRepositoryResult.DayNotFound)
            throw new NotFoundException(typeof(Trip), id);
    }

    public async Task<DayDto> UpdateForTrip(string tripId, string id, UpdateDayDto dto)
    {
        var day = dto.ToEntity(id);
        var result = await _dayRepository.UpdateForTrip(tripId, day);
        
        return result switch
        {
            DayRepositoryResult.DayNotFound => throw new NotFoundException(typeof(Day),id),
            DayRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip),id),
            DayRepositoryResult.Success => day.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }
}