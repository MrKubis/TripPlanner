using backend.Application.DTOs;
using backend.Domain.Entities;

namespace backend.Application.Mappers;

public static class LocationMapper
{
    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };
    }
}