namespace backend.Application.DTOs;

public class DestinationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public LocationDto Location { get; set; }
    public List<string> LinkIds { get; set; }
}

public class CreateDestinationDto
{
    public string Name { get; set; }
    public CreateLocationDto Location { get; set; }
}

public class UpdateDestinationDto
{
    public string? Name { get; set; }
    public UpdateLocationDto? Location { get; set; }
}