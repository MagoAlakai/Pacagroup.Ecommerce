WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("NorthwindConnection");
if (string.IsNullOrEmpty(cs))
{
    throw new InvalidOperationException("Connection string 'NorthwindConnection' is null or empty");
}
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Capa de aplicación (incluye AutoMapper de Application.Main)
builder.Services.AddApplication(builder.Configuration);
// Capa de infraestructura (DbContext, UnitOfWork, repos, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app;
try
{
    app = builder.Build();
}
catch (Exception ex)
{
    Console.WriteLine("ERROR BUILDING APP:");
    Console.WriteLine(ex);
    throw;
}

//var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
