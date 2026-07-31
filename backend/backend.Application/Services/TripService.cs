using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Common.Results;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Domain.Specifications;

namespace backend.Application.Services;


public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<Result<Pagination<TripDto>>> GetPaginationAsync(CatalogSpecParams catalogSpecParams)
    {
        var trips = await _tripRepository.GetPaginationAsync(catalogSpecParams);
        var result = new Pagination<TripDto>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            trips.Count,
            trips.Select(trip => trip.ToDto()).ToList());
     
        return Result<Pagination<TripDto>>.Success(result);
    }

    public async Task<Result<TripDetailsDto>> GetByIdAsync(Guid id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        return trip == null ?
            Result<TripDetailsDto>.Failure("Trip Not Found", ErrorType.NotFound) :
            Result<TripDetailsDto>.Success(trip.ToDetailsDto());
    }

    public async Task<Result<TripDetailsDto>> Create(CreateTripDto dto)
    {
        var trip = dto.ToEntity();
        // todo validation 
        
        var result = await _tripRepository.CreateAsync(trip);
        
        return Result<TripDetailsDto>.Success(result.ToDetailsDto());
    }

    public async Task<Result> Delete(Guid id)
    {
        var result = await _tripRepository.DeleteAsync(id);
        return result ?
            Result.Success() :
            Result.Failure("Trip Not Found");
    }

    public async Task<Result> Update(Guid id,UpdateTripDto dto)
    {
        var trip = await _tripRepository.GetByIdAsync(id);

        if (trip == null) return Result.Failure("Trip Not Found", ErrorType.NotFound);
        
        trip.Description = dto.Description ?? trip.Description;
        trip.Title = dto.Title ?? trip.Title;
        
        var result = await _tripRepository.UpdateAsync(trip);
    
        return result ?
            Result.Success() :
            Result.Failure("Trip Not Found", ErrorType.NotFound);
    }
}