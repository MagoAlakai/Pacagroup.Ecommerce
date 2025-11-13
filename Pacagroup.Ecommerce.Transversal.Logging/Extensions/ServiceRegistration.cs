namespace Pacagroup.Ecommerce.Transversal.Logging.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddLoggingTransversal(this IServiceCollection services, IConfiguration configuration)
    {
        var env = configuration["ASPNETCORE_ENVIRONMENT"]
                  ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Pacagroup.Ecommerce")
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.File(
                path: "Logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}");

        // ❌ No intentes escribir logs en SQL cuando corras en Docker
        if (!string.Equals(env, "Docker", StringComparison.OrdinalIgnoreCase))
        {
            var conn = configuration.GetConnectionString("NorthwindConnection");

            if (!string.IsNullOrWhiteSpace(conn))
            {
                loggerConfig = loggerConfig.WriteTo.MSSqlServer(
                    connectionString: conn,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    },
                    restrictedToMinimumLevel: LogEventLevel.Warning);
            }
        }

        Log.Logger = loggerConfig.CreateLogger();

        services.AddSerilog();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

        return services;
    }
}
