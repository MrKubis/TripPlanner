using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Entities;

public class Link
{
    [Url]
    public string Url;
    public string Title;
}