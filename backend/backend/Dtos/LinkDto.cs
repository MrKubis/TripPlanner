namespace backend.Dtos;

public class LinkDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
}

public class CreateLinkDto
{
    public required string Url { get; set; }
    public string? Title { get; set; }
}

public class PatchLinkDto
{
    public string? Url { get; set; }
    public string? Title { get; set; }
}