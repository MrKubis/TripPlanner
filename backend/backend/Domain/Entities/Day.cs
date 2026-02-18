namespace backend.Domain.Entities;

public class Day
{
    public DateOnly Date {get; set;}
    public List<Destination> Destinations {get; set;}
}