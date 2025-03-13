using System.Net.Http.Json;

namespace DepartureBoard.Infrastructure.ExternalApi;

public class GroundHandlingApi(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task Post(object? dto)
    {
        await _client.PostAsJsonAsync("/uno/api/v1/order/process-order", dto);
    }
}