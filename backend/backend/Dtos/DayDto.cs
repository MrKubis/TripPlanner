namespace backend.Dtos;

public class DayDto
{
    public DateOnly Date { get; set; }
    public List<DestinationDto> Destinations { get; set; }
}

public class CreateDayDto
{
    public DateOnly Date { get; set; }
}

public class PatchDayDto
{
    public DateOnly Date { get; set; }
}