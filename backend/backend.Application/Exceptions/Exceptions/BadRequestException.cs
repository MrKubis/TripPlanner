namespace backend.Application.Exceptions.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string? message) : base(message)
    {
    }
}