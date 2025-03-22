using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class CheckInHttpClient(HttpClient client) : ICheckInClient
{
    private readonly JsonSerializerOptions _options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task NotifyCheckInStart(int flightId, DateTime checkInEndTime) 
        => await client.PostAsJsonAsync($"start/{flightId}", checkInEndTime, _options);
    
    public async Task NotifyCheckInEnd(int flightId)
        => await client.PostAsync($"end/{flightId}", null);
}