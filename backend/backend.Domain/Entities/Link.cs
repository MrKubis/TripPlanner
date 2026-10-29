namespace backend.Domain.Entities;

public class Link : BaseEntity
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    
    public Trip Trip { get; set; }
    public Guid TripId { get; set; }
}