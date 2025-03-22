using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class PassengerHttpClient(HttpClient client) : IPassengerClient
{
    private readonly JsonSerializerOptions _options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task NotifyFlightCreated(object dto)
        => await client.PostAsJsonAsync("available-flight", dto, _options);

    public async Task NotifyCheckInStart(int flightId, DateTime checkInEndTime)
        => await client.PostAsJsonAsync($"check-in/start/{flightId}", checkInEndTime, _options);

    public async Task NotifyCheckInEnd(int flightId)
        => await client.PostAsync($"check-in/end/{flightId}", null);
}