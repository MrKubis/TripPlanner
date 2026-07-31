using backend.Application.DTOs;
using backend.Domain.Entities;

namespace backend.Application.Mappers;

public static class LinkMapper
{
    public static LinkDto ToDto(this Link link)
    {
        return new LinkDto
        {
            Id = link.Id,
            Title = link.Title,
            Url = link.Url,
        };
    }

    public static Link ToEntity(this CreateLinkDto dto)
    {
        return new Link
        {
            Title = dto.Title ?? string.Empty,
            Url = dto.Url,
        };
    }

    public static Link ToEntity(this UpdateLinkDto dto, Guid id)
    {
        return new Link
        {
            Id = id,
            Title = dto.Title ?? string.Empty,
            Url = dto.Url ?? string.Empty,
        };
    }
}