using Microsoft.Extensions.DependencyInjection;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
public static class SwaggerExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Define el documento principal
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Pacagroup Ecommerce API",
                Version = "v1",
                Description = "API para gestión de clientes y operaciones de ecommerce",
                Contact = new OpenApiContact
                {
                    Name = "Equipo de desarrollo",
                    Email = "soporte@pacagroup.com",
                    Url = new Uri("https://pacagroup.com")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // (Opcional) si tienes comentarios XML para documentar tus endpoints
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);

            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Pega solo el token (Swagger añade 'Bearer ' automáticamente)"
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            c.EnableAnnotations();
        });

        return services;
    }
}
