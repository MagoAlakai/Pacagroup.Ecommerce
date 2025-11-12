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
builder.Services.AddCORS();
builder.Services.AddTransversalCommon();
builder.Services.AddAuth(builder.Configuration);
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
// 👇 Middleware: copia token de query/cookie → Authorization header
app.Use(async (ctx, next) =>
{
    // 1) ?access_token=...
    var qsToken = ctx.Request.Query["access_token"];
    if (!string.IsNullOrWhiteSpace(qsToken))
    {
        ctx.Request.Headers["Authorization"] = $"Bearer {qsToken}";
    }
    else if (!ctx.Request.Headers.ContainsKey("Authorization")
             && ctx.Request.Cookies.TryGetValue("auth", out var cookieToken)
             && !string.IsNullOrWhiteSpace(cookieToken))
    {
        // 2) Cookie 'auth'
        ctx.Request.Headers["Authorization"] = $"Bearer {cookieToken}";
    }

    await next();
});
app.UseCors("AllowSwagger");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
