using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class PassengerHttpClient(HttpClient client) : IPassengerClient
{
    private readonly HttpClient _client = client;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task NotifyFlightCreated(object dto)
        => await _client.PostAsJsonAsync("available-flight", dto, _options);

    public async Task NotifyCheckInStart(int flightId, DateTime checkInEndTime)
        => await _client.PostAsJsonAsync($"check-in/start/{flightId}", checkInEndTime, _options);

    public async Task NotifyCheckInEnd(int flightId)
        => await _client.PostAsync($"check-in/end/{flightId}", null);
}