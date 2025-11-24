using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1) DbContext de EF Core
        string? connectionString = configuration.GetConnectionString("NorthwindConnection");

        services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

        // 2) Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // 3) Repositorios concretos (si los inyectas directamente)
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IPasswordHasher<User>), typeof(PasswordHasher<User>));

        return services;
    }
}
