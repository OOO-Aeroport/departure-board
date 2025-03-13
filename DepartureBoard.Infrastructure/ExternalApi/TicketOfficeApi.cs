using System.Net.Http.Json;

namespace DepartureBoard.Infrastructure.ExternalApi;

public class TicketOfficeApi(HttpClient client)
{
    private readonly HttpClient _client = client;
    
    public async Task Post(object? dto)
    {
        await _client.PostAsJsonAsync("/flights", dto);
    }
}