namespace Pacagroup.Ecommerce.Services.WebApi.Modules.GlobalExceptions;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string message = ex.Message.ToString();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            logger.LogError($"An unhandled exception occurred: {message}");

            Response<Object> response = new()
            {
                Message = message
            };

            await JsonObjectSerializer.Default.SerializeAsync(
                context.Response.Body,
                response,
                typeof(Response<object>),
                context.RequestAborted
            );
        }
    }
}
