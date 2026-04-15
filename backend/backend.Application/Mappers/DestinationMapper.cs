using backend.Application.DTOs;
using backend.Domain.Entities;

namespace backend.Application.Mappers;

public static class DestinationMapper
{
    public static DestinationDto ToDto(this Destination destination)
    {
        return new DestinationDto
        {
            Id = destination.Id,
            Name = destination.Name,
            LinkIds = destination.LinkIds,
            Location = destination.Location.ToDto()
        };
    }

    public static Destination ToEntity(this CreateDestinationDto dto)
    {
        return new Destination
        {
            Name = dto.Name,
            Location = new Location
            {
                Longitude = dto.Location.Longitude,
                Latitude = dto.Location.Latitude
            }
        };
    }

    public static Destination ToEntity(this UpdateDestinationDto dto, string id)
    {
        return new Destination
        {
            Id = id,
            Name = dto.Name,
            Location = new Location
            {
                Longitude = dto.Location.Longitude,
                Latitude = dto.Location.Latitude
            }
        };
    }
}