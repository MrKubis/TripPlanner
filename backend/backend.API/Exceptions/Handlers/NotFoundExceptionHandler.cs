using backend.Application.Exceptions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Exceptions.Handlers;

public class NotFoundExceptionHandler(
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        var message = notFoundException.Message;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        var context = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Detail = message,
                Status = StatusCodes.Status404NotFound,
            }
        };
        await problemDetailsService.TryWriteAsync(context);
        return true;
    }
}