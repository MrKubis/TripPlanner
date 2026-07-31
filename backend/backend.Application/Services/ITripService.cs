using backend.Application.DTOs;
using backend.Domain.Specifications;
using backend.Domain.Common.Results;

namespace backend.Application.Services;

public interface ITripService
{
    Task<Result<Pagination<TripDto>>> GetPaginationAsync(CatalogSpecParams catalogSpecParams);
    Task<Result<TripDetailsDto>> Create(CreateTripDto dto);
    Task<Result> Update(Guid id, UpdateTripDto dto);
    Task<Result<TripDetailsDto>> GetByIdAsync(Guid id);
    Task<Result> Delete(Guid id);
}