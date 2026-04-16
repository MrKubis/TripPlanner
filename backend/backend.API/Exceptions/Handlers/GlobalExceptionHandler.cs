using backend.Application.Exceptions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace backend.API.Exceptions.Handlers;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var isAlreadyHandled = exception switch
        {
            NotFoundException => true,
            _ => false
        };

        if (isAlreadyHandled) return true;
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        logger.LogError(exception,exception.Message);

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An exception occured",
                Detail = exception.Message
            }
        });
        return true;
    }

}