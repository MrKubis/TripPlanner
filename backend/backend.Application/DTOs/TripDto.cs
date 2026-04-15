namespace backend.Application.DTOs;

public class TripDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class TripDetailsDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; }
    public List<DestinationDto> Destinations { get; set; }
    public List<LinkDto> Links { get; set; }
    public List<DayDto> Days { get; set; }
    public List<ExpenseDto> Expenses { get; set; }
}

public class CreateTripDto
{
    public required string Title { get; set; }
    public string Description { get; set; }
}

public class UpdateTripDto
{
    public required string Title { get; set; }
    public string Description { get; set; }
}
