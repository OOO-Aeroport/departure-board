using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Misc;

namespace DepartureBoard.Infrastructure.ExternalApi;

public class GroundHandlingApi(HttpClient client, DtoBuffer<GroundHandlingApi> buffer)
{
    private readonly HttpClient _client = client;
    private readonly DtoBuffer<GroundHandlingApi> _buffer = buffer;
    private readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task Post(object dto)
    {
        _buffer.Add(dto);
        var itemsToSend = _buffer.ToList();

        foreach (var item in itemsToSend)
        {
            var response = await _client.PostAsJsonAsync("uno/api/v1/order/process-order", item, _options);
            
            if (response.IsSuccessStatusCode)
            {
                _buffer.Remove(item);
            }
        }
    }
}