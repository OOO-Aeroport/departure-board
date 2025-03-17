using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class PassengerHttpClient(HttpClient client) : IPassengerClient
{
    private readonly HttpClient _client = client;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task Post(object dto)
        => await _client.PostAsJsonAsync("", dto, _options);
}