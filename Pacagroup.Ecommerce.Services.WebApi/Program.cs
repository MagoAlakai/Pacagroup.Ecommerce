WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("NorthwindConnection");
if (string.IsNullOrEmpty(cs))
{
    throw new InvalidOperationException("Connection string 'NorthwindConnection' is null or empty");
}
// Add services to the container.
builder.Services.AddControllers();

// Add from ServiceCollectionExtension
builder.Services.AddSwagger();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.ShowExtensions();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
