namespace backend.Application.DTOs;

public class DayDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
}

public class CreateDayDto
{
    public DateOnly Date { get; set; }
    public Guid TripId { get; set; }
}

public class UpdateDayDto
{
    public DateOnly Date { get; set; }
}