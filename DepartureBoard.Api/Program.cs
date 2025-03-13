using DepartureBoard.Api.Middleware;
using DepartureBoard.App.Services;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;
using DepartureBoard.Infrastructure.Repos.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Logging Configuration
builder.Logging
    .ClearProviders()
    .AddConsole();
builder.Services.AddLogging();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options
    => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Departure Board", Version = "v1" }));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Repos configuration
builder.Services.AddScoped<IRepository<Airplane>, EfAirplaneRepository>();
builder.Services.AddScoped<IRepository<Flight>, EfFlightRepository>();

// Hosted services configuration

// Services configuration
builder.Services.AddSingleton<TimeService>();
builder.Services.AddTransient<FlightService>();

var app = builder.Build();

// Starting timer
var timeService = app.Services.GetRequiredService<TimeService>();
_ = Task.Run(() => timeService.Start());

//app.UseHttpsRedirection();
app.UseMiddleware<LoggerMiddleware>();
app.MapEndpoints();

app.Run();