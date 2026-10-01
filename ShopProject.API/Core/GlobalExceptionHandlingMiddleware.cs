using ShopProject.Application;
using ShopProject.Application.Exceptions;
using FluentValidation;

namespace ShopProject.API.Core
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private IErrorLogger _logger;
        private IApplicationActor _actor;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IApplicationActor actor)
        {
            _next = next;
            //_logger = logger;
            _actor = actor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                if (exception is UnauthorizedAccessException)
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                if (exception is ValidationException ex)
                {
                    httpContext.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if (exception is EntityNotFoundException notFound)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    await httpContext.Response.WriteAsJsonAsync(new { error = notFound.Message });
                    return;
                }

                if (exception is ConflictException conflict)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    await httpContext.Response.WriteAsJsonAsync(new { error = conflict.Message });
                    return;
                }

                //var errorId = _logger.Log(exception, _actor);

                Console.WriteLine("=== UNHANDLED EXCEPTION ===");
                Console.WriteLine(exception.ToString());

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support with this ID - ." });
            }
        }
    }
}
