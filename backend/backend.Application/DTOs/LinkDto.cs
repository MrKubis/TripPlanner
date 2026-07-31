namespace backend.Application.DTOs;

public record LinkDto : BaseEntityDto
{
    public string Url { get; set; }
    public string Title { get; set; }
}

public record CreateLinkDto
{
    public required string Url { get; set; }
    public string? Title { get; set; }
}

public record UpdateLinkDto
{
    public string? Url { get; set; }
    public string? Title { get; set; }
}