namespace Pacagroup.Ecommerce.Transversal.Common.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddTransversalCommon(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
