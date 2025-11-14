namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Validator;

public static class ValidatorExtension
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<SignUpDTOValidator>();
        services.AddTransient<SignInDTOValidator>();
        services.AddTransient<UserDTOValidator>();
        services.AddTransient<CustomerDTOValidator>();

        return services;
    }
}
