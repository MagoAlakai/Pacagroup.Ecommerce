using Pacagroup.Ecommerce.Aplicacion.Main.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Capa de aplicación (incluye AutoMapper de Application.Main)
builder.Services.AddApplication(builder.Configuration);
// Capa de infraestructura (DbContext, UnitOfWork, repos, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
