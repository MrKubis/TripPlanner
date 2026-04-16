namespace backend.Application.Exceptions.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type type, string message) : base($"{type.Name},{message}") { }
}
