namespace backend.Application.DTOs;

public record DestinationDto : BaseEntityDto
{
    public string Name { get; set; }
    public LocationDto Location { get; set; }
}

public record CreateDestinationDto
{
    public string Name { get; set; }
    public CreateLocationDto Location { get; set; }
}

public record UpdateDestinationDto
{
    public string? Name { get; set; }
    public UpdateLocationDto? Location { get; set; }
}