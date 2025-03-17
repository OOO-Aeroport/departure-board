using DepartureBoard.Api.Factories;
using DepartureBoard.Api.Options;
using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Network.Factories;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Application.UseCases;
using DepartureBoard.Infrastructure.Network;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DepartureBoard.Api", Version = "v1" });
});

// Client factories registration
builder.Services.AddSingleton<ICheckInClientFactory, CheckInClientFactory>();

// Client options configuration
builder.Services.Configure<TicketOfficeClientOptions>(
    builder.Configuration.GetSection("Clients:TicketOfficeHttpClient"));
builder.Services.Configure<PassengerClientOptions>(
    builder.Configuration.GetSection("Clients:PassengerHttpClient"));
builder.Services.Configure<CheckInClientOptions>(
    builder.Configuration.GetSection("Clients:CheckInHttpClient"));

// Clients registration
builder.Services.AddHttpClient<ITicketOfficeClient, TicketOfficeHttpClient>(
    (provider, client) =>
    {
        var options = provider.GetRequiredService<IOptions<TicketOfficeClientOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseAddress);
    });
builder.Services.AddHttpClient<IPassengerClient, PassengerHttpClient>(
    (provider, client) =>
    {
        var options = provider.GetRequiredService<IOptions<PassengerClientOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseAddress);
    });
builder.Services.AddHttpClient<ICheckInClient, CheckInHttpClient>(
    (provider, client) =>
    {
        var options = provider.GetRequiredService<IOptions<CheckInClientOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseAddress);
    });

// DbContext registration
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AirplaneFlightDbContext>(options =>
    options.UseNpgsql(connectionString));

// Time service registration
builder.Services.AddSingleton<TimeService>();

// Repositories registration
builder.Services.AddScoped<IAirplaneRepository, EfAirplaneRepository>();
builder.Services.AddScoped<IFlightRepository, EfFlightRepository>();
builder.Services.AddScoped<IAirplaneFlightUnitOfWork, EfAirplaneFlightUnitOfWork>();

// Use cases registration
builder.Services.AddTransient<RegisterFlightUseCase>();
builder.Services.AddTransient<ScheduleCheckInUseCase>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "DepartureBoard.Api v1");
    options.RoutePrefix = "";
});

// Starting timer
var timeService = app.Services.GetRequiredService<TimeService>();
_ = Task.Run(() => timeService.Run());

app.MapControllers();
app.Run();