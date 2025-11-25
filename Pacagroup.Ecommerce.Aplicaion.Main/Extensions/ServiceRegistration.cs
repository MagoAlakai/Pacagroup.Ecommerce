namespace Pacagroup.Ecommerce.Aplicacion.Main.Extensions;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<ICustomerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

        return services;
    }
}
