using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.App.Ports.Network;
using DepartureBoard.Misc;

namespace DepartureBoard.Infrastructure.Adapters.Network;

public class TicketOfficeHttpClient(HttpClient client, DtoBuffer<TicketOfficeHttpClient> buffer) : ITicketOfficeClient
{
    private readonly HttpClient _client = client;
    private readonly DtoBuffer<TicketOfficeHttpClient> _buffer = buffer;
    private readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task Post(object dto)
    {
        _buffer.Add(dto);
        var itemsToSend = _buffer.ToList();
        
        foreach (var item in itemsToSend)
        {
            var response = await _client.PostAsJsonAsync("ticket-office/flights", item, _options);

            if (response.IsSuccessStatusCode)
            {
                _buffer.Remove(item);
            }
        }
    }
}