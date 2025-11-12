namespace Pacagroup.Ecommerce.Services.WebApi.Modules.CORS;

public static class CorsExtension
{
    public static IServiceCollection AddCORS(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSwagger", builder =>
            {
                builder.WithOrigins("https://localhost:7087", "http://localhost:5256")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        return services;
    }
}
