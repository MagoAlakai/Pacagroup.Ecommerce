
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICustomerDomain, CustomerDomain>();

