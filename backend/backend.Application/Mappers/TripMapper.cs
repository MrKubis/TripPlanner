using backend.Application.DTOs;
using backend.Domain.Entities;
using backend.Domain.Specifications;

namespace backend.Application.Mappers;

public static class TripMapper
{
    public static Pagination<TripDto> ToDto(this Pagination<Trip> pagination)
    {
        return new Pagination<TripDto>
        {
            Count = pagination.Count,
            PageIndex = pagination.PageIndex,
            PageSize = pagination.PageSize,
            Data = pagination.Data.Select(trip => trip.ToDto()).ToList()
        };
    }

    public static TripDto ToDto(this Trip trip)
    {
        return new TripDto
        {
            Id = trip.Id,
            Title = trip.Title,
            CreatedBy = trip.CreatedBy,
            CreatedOn = trip.CreatedOn,
        };
    }

    public static TripDetailsDto ToDetailsDto(this Trip trip)
    {
        return new TripDetailsDto
        {
            Id = trip.Id,
            Title = trip.Title,
            CreatedBy = trip.CreatedBy,
            CreatedOn = trip.CreatedOn,
            Description = trip.Description,
            Destinations = trip.Destinations
                .Select(d => d.ToDto())
                .ToList(),
            Days = trip.Days
                .Select(d => d.ToDto())
                .ToList(),
            Expenses = trip.Expenses
                .Select(e => e.ToDto())
                .ToList(),
            Links = trip.Links
                .Select(l => l.ToDto())
                .ToList()
        };
    }

    public static Trip ToEntity(this CreateTripDto dto)
    {
        return new Trip
        {
            Title = dto.Title,
            Description = dto.Description,
            Links = new List<Link>(),
            Days = new List<Day>(),
            Destinations = new List<Destination>(),
            Expenses = new List<Expense>(),
            CreatedOn = DateTime.UtcNow,
            CreatedBy = null
        };
    }
}