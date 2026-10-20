using ShopProject.Application;
using ShopProject.Application.Exceptions;
using FluentValidation;

namespace ShopProject.API.Core
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IErrorLogger logger, IApplicationActor actor)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
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
                    httpContext.Response.StatusCode = 404;
                    await httpContext.Response.WriteAsJsonAsync(new { error = notFound.Message });
                    return;
                }

                if (exception is ConflictException conflict)
                {
                    httpContext.Response.StatusCode = 409;
                    await httpContext.Response.WriteAsJsonAsync(new { error = conflict.Message });
                    return;
                }
                
                Guid? errorId = null;
                try
                {
                    errorId = logger.Log(exception, actor);
                }
                catch (Exception logException)
                {
                    Console.WriteLine("=== ERROR LOGGING FAILED ===");
                    Console.WriteLine(logException.ToString());
                }

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support with this ID - {errorId}." });
            }
        }
    }
}
