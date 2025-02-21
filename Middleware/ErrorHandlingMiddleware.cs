
using TradingSystemApi.Exceptions;

namespace ChatAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                _logger.LogError(forbiddenException, forbiddenException.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (ConflictException conflictException)
            {
                _logger.LogError(conflictException, conflictException.Message);

                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(conflictException.Message);
            }        
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong: " + e.Message);
            }
        }
    }
}
