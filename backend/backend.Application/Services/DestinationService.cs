using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using MongoDB.Driver;

namespace backend.Application.Services;

public class DestinationService
{
    private IDestinationRepository _destinationRepository;
    
    public DestinationService(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository;
    }

    public async Task<DestinationDto> CreateForTrip(string tripId, CreateDestinationDto dto)
    {
        var destination = dto.ToEntity();
        var result = await _destinationRepository.CreateForTrip(tripId, destination);
        return result switch
        {
            DestinationRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            DestinationRepositoryResult.Success => destination.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }

    public async Task DeleteForTrip(string tripId,string id)
    {
        var result = await _destinationRepository.DeleteForTrip(tripId, id);
        if (result == DestinationRepositoryResult.TripNotFound)
        {
            throw new NotFoundException(typeof(Trip), tripId);
        }
        if(result == DestinationRepositoryResult.DestinationNotFound)
        {
            throw new NotFoundException(typeof(Destination), id);
        }
    }

    public async Task<DestinationDto> UpdateForTrip(string tripId, string id, UpdateDestinationDto dto)
    {
        var destination = dto.ToEntity(id);
        var result = await _destinationRepository.UpdateForTrip(tripId, destination);
        return result switch
        {
            DestinationRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            DestinationRepositoryResult.DestinationNotFound => throw new NotFoundException(typeof(Destination), id),
            DestinationRepositoryResult.Success => destination.ToDto(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<DestinationDto> CreateForDay(string tripId, string dayId, CreateDestinationDto dto)
    {
        var destination = dto.ToEntity();
        var result = await _destinationRepository.CreateForDay(tripId, dayId, destination);
        return result switch
        {
            DestinationRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            DestinationRepositoryResult.DayNotFound => throw new NotFoundException(typeof(Day), dayId),
            DestinationRepositoryResult.Success => destination.ToDto(),
            _ => throw new ArgumentOutOfRangeException(null)
        };
    }
    public async Task AppendDestinationToDay(string tripId, string dayId, string destinationId)
    {
        var result = await _destinationRepository.AppendToDay(tripId, dayId, destinationId);
        switch (result)
        {
            case DestinationRepositoryResult.TripNotFound : throw new NotFoundException(typeof(Trip), tripId);
            case DestinationRepositoryResult.DayNotFound : throw new NotFoundException(typeof(Day), dayId);
            case DestinationRepositoryResult.DestinationNotFound : throw new NotFoundException(typeof(Destination), destinationId);
        }
    }

    public async Task RemoveDestinationFromDay(string tripId, string dayId, string destinationId)
    {
        var result = await _destinationRepository.RemoveFromDay(tripId, dayId, destinationId);
        switch (result)
        {
            case DestinationRepositoryResult.TripNotFound : throw new NotFoundException(typeof(Trip), tripId);
            case DestinationRepositoryResult.DayNotFound : throw new NotFoundException(typeof(Day), dayId);
            case DestinationRepositoryResult.DestinationNotFound : throw new NotFoundException(typeof(Destination), destinationId);
        }
        
    }
}
