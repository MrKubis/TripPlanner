namespace backend.Application.DTOs;

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

public class UpdateLinkDto
{
    public string? Url { get; set; }
    public string? Title { get; set; }
}