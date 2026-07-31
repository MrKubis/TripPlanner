using backend.Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(new { error = result.Error }),
            ErrorType.BadRequest or ErrorType.Generic => new BadRequestObjectResult(new { error = result.Error }),
            _ => new ObjectResult(result.Error) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }
    
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new NoContentResult();
        }

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(new { error = result.Error }),
            ErrorType.BadRequest or ErrorType.Generic => new BadRequestObjectResult(new { error = result.Error }),
            _ => new ObjectResult(new { error = result.Error })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }
}