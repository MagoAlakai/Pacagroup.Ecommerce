namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Midddlewares;

public static class ServiceRegistration
{
    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandler>();

        return services;
    }
}
