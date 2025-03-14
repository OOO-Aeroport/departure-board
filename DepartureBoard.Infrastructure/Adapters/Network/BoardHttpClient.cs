using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.App.Ports.Network;

namespace DepartureBoard.Infrastructure.Adapters.Network;

public class BoardHttpClient(HttpClient client) : IBoardClient
{
    private readonly HttpClient _client = client;
    private readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
    
    public async Task Post(int id, List<object> dtos)
    {
        await _client.PostAsJsonAsync($"reg_passengers/{id}", dtos, _options);
    }
}