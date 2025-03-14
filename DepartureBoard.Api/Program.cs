using DepartureBoard.Api;
using DepartureBoard.Api.Middleware;
using DepartureBoard.App.Ports.Network;
using DepartureBoard.App.Ports.Persistence;
using DepartureBoard.App.Scenarios;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;
using DepartureBoard.Infrastructure.Adapters.Network;
using DepartureBoard.Infrastructure.Adapters.Persistence.EntityFramework;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;
using DepartureBoard.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Logging Configuration
builder.Logging
    .ClearProviders()
    .AddSimpleConsole(options =>
    {
        options.IncludeScopes = false;
        options.SingleLine = true;
    });

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

// Scenarios configuration
builder.Services.AddScoped<RegisterFlightScenario>();
builder.Services.AddScoped<HandleAirplaneScenario>();
builder.Services.AddScoped<SendPassengersToBoardScenario>();

// Clients configuration
builder.Services.AddTransient<ITicketOfficeClient, TicketOfficeHttpClient>();
builder.Services.AddTransient<IGroundHandlingClient, GroundHandlingHttpClient>();
builder.Services.AddTransient<IBoardClient, BoardHttpClient>();

// Buffers configuration
builder.Services.AddSingleton<DtoBuffer<TicketOfficeHttpClient>>();
builder.Services.AddSingleton<DtoBuffer<GroundHandlingHttpClient>>();

// HttpClients configuration
builder.Services.AddHttpClient<TicketOfficeHttpClient>(client
    => client.BaseAddress = new Uri(RequireUrl("ExternalApiSettings:TicketOfficeBaseUrl")));
builder.Services.AddHttpClient<GroundHandlingHttpClient>(client
    => client.BaseAddress = new Uri(RequireUrl("ExternalApiSettings:GroundHandlingBaseUrl")));
builder.Services.AddHttpClient<BoardHttpClient>(client
    => client.BaseAddress = new Uri(RequireUrl("ExternalApiSettings:BoardBaseUrl")));

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

return;

string RequireUrl(string configurationProperty) 
    => builder.Configuration.GetValue<string>(configurationProperty)
       ?? throw new NullReferenceException();
       