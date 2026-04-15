namespace backend.Application.DTOs;

public class DayDto
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public List<string> DestinationIds { get; set; }
}

public class CreateDayDto
{
    public DateOnly Date { get; set; }
}

public class UpdateDayDto
{
    public DateOnly Date { get; set; }
    public List<string> DestinationIds { get; set; }
}