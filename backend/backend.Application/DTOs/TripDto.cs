namespace backend.Application.DTOs;

public record TripDto : BaseEntityDto
{
    public string Title { get; set; }
}

public record TripDetailsDto : BaseEntityDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<DestinationDto> Destinations { get; set; }
    public List<LinkDto> Links { get; set; }
    public List<DayDto> Days { get; set; }
    public List<ExpenseDto> Expenses { get; set; }
}

public record CreateTripDto
{
    public required string Title { get; set; }
    public string Description { get; set; }
}

public record UpdateTripDto
{
    public required string Title { get; set; }
    public string Description { get; set; }
}
