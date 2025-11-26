namespace Pacagroup.Ecommerce.Aplicacion.Main.Extensions;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

        services.AddTransient<GetAllCustomersHandler>();
        services.AddTransient<GetCustomerHandler>();
        services.AddTransient<CreateCustomerHandler>();
        services.AddTransient<UpdateCustomerHandler>();
        services.AddTransient<DeleteCustomerHandler>();

        services.AddTransient<CreateUserHandler>();
        services.AddTransient<IsValidUserHandler>();

        return services;
    }
}
