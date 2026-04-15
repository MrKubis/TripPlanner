using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Domain.Specifications;
using MongoDB.Driver;

namespace backend.Application.Services;


public class TripService
{
    private readonly ITripRepository _tripRepository;
    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<Pagination<TripDto>> GetAllAsync(CatalogSpecParams catalogSpecParams)
    {
        var trips = await _tripRepository.GetAllAsync(catalogSpecParams);
        return trips.ToDto();
    }

    public async Task<TripDetailsDto> GetByIdAsync(string id)
    {
        var result = await _tripRepository.GetByIdAsync(id);
        if (result == null)
        {
            throw new NotFoundException(typeof(Trip),id);
        }

        return result.ToDetailsDto();
    }

    public async Task<TripDetailsDto> Create(CreateTripDto dto)
    {
        var trip = dto.ToEntity();
        var result = await _tripRepository.CreateAsync(trip);
        return result.ToDetailsDto();
    }

    public async Task Delete(string id)
    {
        var result = await _tripRepository.DeleteAsync(id);
        if (!result)
        {
            throw new NotFoundException(typeof(Trip),id);
        }
    }

    public async Task Update(string id,UpdateTripDto dto)
    {
        var updateTrip = new Trip
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
        };
        var isUpdated = await _tripRepository.UpdateAsync(id, updateTrip);
        if (!isUpdated)
        {
            throw new NotFoundException(typeof(Trip),id);
        }
    }
}