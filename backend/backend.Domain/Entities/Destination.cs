namespace backend.Domain.Entities;

public class Destination : BaseEntity
{
    public string Name { get; set; }
    public Location Location { get; set; }
    
    public Guid TripId { get; set; }
    public Trip Trip {get; set;}

    public Guid? DayId { get; set; }
    public Day? Day {get; set;}
    
    public virtual ICollection<Link> Links { get; set; }
}