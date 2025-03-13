using DepartureBoard.Api.Middleware;
using DepartureBoard.App;
using DepartureBoard.App.Services;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.ExternalApi;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;
using DepartureBoard.Infrastructure.Repos.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Logging Configuration
builder.Logging
    .ClearProviders()
    .AddConsole();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options
    => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Departure Board", Version = "v1" }));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Repos configuration
builder.Services.AddScoped<IRepository<Airplane>, EfAirplaneRepository>();
builder.Services.AddScoped<IRepository<Flight>, EfFlightRepository>();
builder.Services.AddScoped<IAirplaneAndFlightUnitOfWork, EfAirplaneAndFlightUnitOfWork>();

// Services configuration
builder.Services.AddSingleton<TimeService>();
builder.Services.AddScoped<FlightService>();

// External API configuration
builder.Services.AddTransient<TicketOfficeApi>();
builder.Services.AddTransient<GroundHandlingApi>();

// HttpClients configuration
    
    // Ticket office
var ticketOfficeBaseUrl = builder.Configuration.GetValue<string>("ExternalApiSettings:TicketOfficeBaseUrl")
    ?? throw new Exception("TicketOfficeBaseUrl is missing");
builder.Services.AddHttpClient<TicketOfficeApi>(client
    => client.BaseAddress = new Uri(ticketOfficeBaseUrl));

    // Ground handling
var groundHandlingBaseUrl = builder.Configuration.GetValue<string>("ExternalApiSettings:GroundHandlingBaseUrl")
    ?? throw new Exception("GroundHandlingBaseUrl is missing");
builder.Services.AddHttpClient<GroundHandlingApi>(client
    => client.BaseAddress = new Uri(groundHandlingBaseUrl));
var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

// Swagger configuration
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Departure Board API V1");
    options.RoutePrefix = string.Empty;
});

// Starting timer
var timeService = app.Services.GetRequiredService<TimeService>();
_ = Task.Run(() => timeService.Start());

app.UseMiddleware<RequestLogger>();
app.MapEndpoints();

app.Run();