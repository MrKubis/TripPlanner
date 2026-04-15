using backend.Domain.Entities;
using backend.Domain.Specifications;

namespace backend.Domain.Repositories;

public interface ITripRepository
{
   Task<Pagination<Trip>> GetAllAsync(CatalogSpecParams catalogSpecParams);
   Task<Trip?> GetByIdAsync(string tripId);
   Task<Trip> CreateAsync(Trip trip);
   Task<bool> DeleteAsync(string tripId);
   Task<bool> UpdateAsync(string tripId,Trip trip);
}