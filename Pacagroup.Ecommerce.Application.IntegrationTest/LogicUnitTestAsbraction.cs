using Pacagroup.Ecommerce.Transversal.Logging.Extensions;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Validator;

namespace Pacagroup.Ecommerce.Application.Test;

public abstract class LogicUnitTestAbstraction
{
    protected IConfiguration Configuration { get; private set; } = default!;
    protected IServiceCollection Services { get; private set; } = default!;
    protected IServiceProvider ServiceProvider { get; private set; } = default!;
    protected IServiceScopeFactory ScopeFactory { get; private set; } = default!;
    public TestContext? TestContext { get; set; }

    [TestInitialize]
    public void TestInitialize()
    {
        // 1) Configuración (puedes usar appsettings.Test.json si quieres)
        var basePath = Path.GetDirectoryName(typeof(LogicUnitTestAbstraction).Assembly.Location)!;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        // 2) DI container
        Services = new ServiceCollection();

        // (opcional) logging para ver mensajes en test
        Services.AddLogging(b => b.AddDebug().AddConsole());

        Services.AddSingleton<IConfiguration>(Configuration);
        Services.AddLoggingTransversal(Configuration);
        Services.AddValidator();

        // 3) Tus extensiones exactamente como en Program.cs
        Services.AddApplication(Configuration);
        Services.AddInfrastructure(Configuration);
        Services.AddTransversalCommon();

        // Si en tests quieres una BD en memoria, sustituye aquí:
        // Services.ReplaceDbContextWithInMemory<AppDbContext>("TestsDb");

        // 4) Build provider + scope factory
        ServiceProvider = Services.BuildServiceProvider(validateScopes: true);
        ScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    protected T GetRequired<T>() where T : notnull =>
        ServiceProvider.GetRequiredService<T>();
}
