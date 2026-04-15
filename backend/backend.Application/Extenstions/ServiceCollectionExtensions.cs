using backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application.Extenstions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<DayService>();
        services.AddScoped<TripService>();
        services.AddScoped<DestinationService>();
        services.AddScoped<LinkService>();
    }
}