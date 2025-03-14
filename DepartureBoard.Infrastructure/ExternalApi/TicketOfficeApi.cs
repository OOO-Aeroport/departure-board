using System.Net.Http.Json;
using System.Text.Json;

namespace DepartureBoard.Infrastructure.ExternalApi;

public class TicketOfficeApi(HttpClient client)
{
    private readonly HttpClient _client = client;
    
    public async Task Post(object? dto)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        await _client.PostAsJsonAsync("ticket-office/flights", dto, jsonSerializerOptions);
    }
}