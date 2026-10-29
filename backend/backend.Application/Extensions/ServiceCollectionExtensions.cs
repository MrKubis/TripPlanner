using backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDayService , DayService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IDestinationService, DestinationService>();
        services.AddScoped<ILinkService, LinkService>();
    }
}
