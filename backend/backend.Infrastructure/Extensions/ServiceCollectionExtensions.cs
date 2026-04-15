using backend.Domain.Entities;
using backend.Domain.Repositories;
using backend.Infrastructure.Repositories;
using backend.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
   public static void AddInfrastructure(this IServiceCollection services)
   {
      services.AddScoped<ILinkRepository, LinkRepository>();
      services.AddScoped<ITripRepository, TripRepository>();
      services.AddScoped<IDayRepository, DayRepository>();
      services.AddScoped<IDestinationRepository, DestinationRepository>();
      

   }
}