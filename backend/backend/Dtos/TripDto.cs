namespace backend.Dtos;

public class TripDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class CreateTripDto
{
    public required string Title { get; set; }
}