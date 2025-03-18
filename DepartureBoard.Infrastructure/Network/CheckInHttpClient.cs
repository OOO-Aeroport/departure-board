using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Dto;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class CheckInHttpClient(HttpClient client) : ICheckInClient
{
    private readonly HttpClient _client = client;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task NotifyCheckInStart(int flightId, DateTime checkInEndTime) 
        => await _client.PostAsJsonAsync($"start/{flightId}", checkInEndTime, _options);
    
    public async Task NotifyCheckInEnd(int flightId)
        => await _client.PostAsync($"end/{flightId}", null);
}