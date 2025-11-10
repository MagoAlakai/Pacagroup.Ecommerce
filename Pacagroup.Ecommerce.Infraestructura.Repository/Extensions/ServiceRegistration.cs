using Pacagroup.Ecommerce.Infraestructura.Interface;

namespace Pacagroup.Ecommerce.Infraestructura.Repository.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1) DbContext de EF Core
        string? connectionString = configuration.GetConnectionString("NorthwindConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // 2) Unit of Work
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        // 3) Repositorios concretos (si los inyectas directamente)
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
