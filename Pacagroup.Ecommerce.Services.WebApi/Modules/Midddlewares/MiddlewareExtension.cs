namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Midddlewares;

public static class MiddlewareExtension
{
    public static IApplicationBuilder AddMiddlewareServices(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandler>();
    }
}
