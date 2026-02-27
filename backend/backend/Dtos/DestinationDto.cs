namespace backend.Dtos;

public class DestinationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public LocationDto Location { get; set; }
    public List<LinkDto> Links { get; set; }
}

public class CreateDestinationDto
{
    public string Name { get; set; }
    public CreateLocationDto Location { get; set; }
}

public class PatchDestinationDto
{
    public string? Name { get; set; }
    public PatchLocationDto? Location { get; set; }
}