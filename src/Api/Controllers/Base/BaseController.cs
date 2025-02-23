using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult SendResult<T>(Result<T> result)
        {
            return result.Match<IActionResult>(
                onSuccess: response => Ok(response),
                onFailure: ex =>
                {
                    if (ex is Domain.Exceptions.BusinessLogicException)
                    {
                        return UnprocessableEntity(new { message = ex.Message });
                    }

                    return BadRequest(new { message = ex.Message });
                },
                onNull: () => NotFound(new {message = "Not found. "})
            );
        }
    }
}
