namespace Pacagroup.Ecommerce.Transversal.Logging.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddLoggingTransversal(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Pacagroup.Ecommerce")
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.File(
                path: "Logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString("NorthwindConnection")!,
                sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true,
                },
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

        return services;
    }
}
