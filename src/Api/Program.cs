using Api.Extensions;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

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