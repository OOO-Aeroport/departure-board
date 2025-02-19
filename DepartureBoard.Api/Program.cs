using DepartureBoard.Api.Middleware;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;
using DepartureBoard.Infrastructure.Repos.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen
    (options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Departure Board", Version = "v1" }));

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Repos configuration
builder.Services.AddScoped<IPlaneRepo, EfPlaneRepo>();
builder.Services.AddScoped<IFlightRepo, EfFlightRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();