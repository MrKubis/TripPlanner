namespace backend.Domain.Entities;

public class Expense : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price {get; set;}
    
    public Trip Trip { get; set; }
    public Guid TripId { get; set; }
}