using Api.Extensions;
using Application;
using Domain.Attributes.Interfaces;
using Domain.Settings;
using Infrastructure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddMongoService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Register custom exception handling middleware
app.UseMiddleware<Api.Middlewares.GlobalExceptionHandlerMiddleware>();

// Swagger should always be used
app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();