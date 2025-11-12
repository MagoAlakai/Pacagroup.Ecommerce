namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var keyStr = configuration["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };

                // Log del motivo del 401 (muy útil)
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine($"[JWT] AuthFailed: {ctx.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        if (!string.IsNullOrEmpty(ctx.ErrorDescription))
                            Console.WriteLine($"[JWT] Challenge: {ctx.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
