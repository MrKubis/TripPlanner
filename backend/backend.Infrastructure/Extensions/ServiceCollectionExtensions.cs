using backend.Domain.Repositories;
using backend.Infrastructure.Persistence;
using backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
   public static void AddInfrastructure(this IServiceCollection services,
      IConfiguration configuration)
   {
      var connectionString = configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AppDbContext>(options =>
         options.UseNpgsql(connectionString, b => 
            b.MigrationsAssembly("backend.Infrastructure"))
         );
      
      services.AddScoped<ILinkRepository, LinkRepository>();
      services.AddScoped<ITripRepository, TripRepository>();
      services.AddScoped<IDayRepository, DayRepository>();
      services.AddScoped<IDestinationRepository, DestinationRepository>();
   }
}