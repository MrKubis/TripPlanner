using backend.Domain.Entities;
using backend.Domain.Specifications;

namespace backend.Domain.Repositories;

public interface ITripRepository
{
   Task<ICollection<Trip>> GetPaginationAsync(CatalogSpecParams catalogSpecParams);
   Task<Trip?> GetByIdAsync(Guid tripId);
   Task<Trip> CreateAsync(Trip trip);
   Task<bool> DeleteAsync(Guid tripId);
   Task<bool> UpdateAsync(Trip trip);
}