namespace backend.Domain.Entities;

public class Day : BaseEntity
{
    public DateOnly Date {get; set;}
    public Guid TripId {get; set;}
    public Trip Trip {get; set;}
    
    public virtual List<Destination> Destinations { get; set; }
}