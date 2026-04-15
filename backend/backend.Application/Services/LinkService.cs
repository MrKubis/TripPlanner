using backend.Application.DTOs;
using backend.Application.Exceptions.Exceptions;
using backend.Application.Mappers;
using backend.Domain.Entities;
using backend.Domain.Repositories;
using MongoDB.Driver;

namespace backend.Application.Services;

public class LinkService
{

    private ILinkRepository _linkRepository;
    public LinkService(IMongoCollection<Trip> trips, ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<LinkDto> CreateForTrip(string tripId, CreateLinkDto dto)
    {
        var link = dto.ToEntity();

        var result = await _linkRepository.CreateForTrip(tripId, link);

        return result switch
        {
            LinkRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            LinkRepositoryResult.Success => link.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }

    public async Task DeleteForTrip(string tripId, string id)
    {
        var result = await _linkRepository.DeleteForTrip(tripId, id);

        if (result == LinkRepositoryResult.TripNotFound)
            throw new NotFoundException(typeof(Trip), tripId);
        if (result == LinkRepositoryResult.LinkNotFound)
            throw new NotFoundException(typeof(Trip), id);
    }

    public async Task<LinkDto> UpdateForTrip(string tripId, string id, UpdateLinkDto dto)
    {
        var link = dto.ToEntity(id);
        var result = await _linkRepository.UpdateForTrip(tripId, link);

        return result switch
        {
            LinkRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            LinkRepositoryResult.LinkNotFound => throw new NotFoundException(typeof(Link), id),
            LinkRepositoryResult.Success => link.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }

    public async Task<LinkDto> CreateForDestination(string tripId, string destinationId, CreateLinkDto dto)
    {
        var link = new Link
        {
            Title = dto.Title,
            Url = dto.Url
        };
        
        var result = await _linkRepository.CreateForDestination(tripId, destinationId, link);

        return result switch
        {
            LinkRepositoryResult.TripNotFound => throw new NotFoundException(typeof(Trip), tripId),
            LinkRepositoryResult.DestinationNotFound => throw new NotFoundException(typeof(Destination), destinationId),
            LinkRepositoryResult.Success => link.ToDto(),
            _ => throw new BadRequestException(null)
        };
    }

    public async Task DeleteForDestination(string tripId, string destinationId, string id)
    {
        var result = await _linkRepository.DeleteForDestination(tripId, destinationId, id);
        switch(result)
        {
           case LinkRepositoryResult.TripNotFound : throw new NotFoundException(typeof(Trip), tripId);
           case LinkRepositoryResult.DestinationNotFound : throw new NotFoundException(typeof(Destination), destinationId);
           case LinkRepositoryResult.Success : return;
        }
    }
}