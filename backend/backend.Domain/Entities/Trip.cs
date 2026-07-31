namespace backend.Domain.Entities;

public class Trip : BaseEntity
{
    public string Title {get; set;}
    public string Description {get; set;}
    
    public virtual ICollection<Destination> Destinations { get; set; }
    public virtual ICollection<Link> Links { get; set; }
    public virtual ICollection<Day> Days { get; set; }
    public virtual ICollection<Expense> Expenses { get; set; }
    
}