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
            Title = dto.Title,
            Url = dto.Url,
        };
    }

    public static Link ToEntity(this UpdateLinkDto dto,string id)
    {
        return new Link
        {
            Id = id,
            Title = dto.Title,
            Url = dto.Url,
        };
    }
}