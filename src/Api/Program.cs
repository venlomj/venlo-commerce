using Api.Extensions;
using Application;
using Domain.Attributes.Interfaces;
using Domain.Settings;
using Infrastructure;
using Infrastructure.Persistence.SQL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

// Register IHttpContextAccessor to resolve the error
builder.Services.AddHttpContextAccessor();

// Add logging services
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();            // Clear default providers
    logging.AddConsole();                // Add Console logging (you can add other providers as needed, e.g., file logging)
    logging.AddDebug();                  // Add Debug logging (optional)
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);  // Set log level if needed (optional)
});

// Add services to the container
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAuthenticationInternal(configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddMongoService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3002");
});

// Register custom exception handling middleware
app.UseMiddleware<Api.Middlewares.GlobalExceptionHandlerMiddleware>();

// Swagger should always be used
app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeeder.SeedAsync(services);
}

app.Run();