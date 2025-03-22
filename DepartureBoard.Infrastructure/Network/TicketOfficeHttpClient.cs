using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class TicketOfficeHttpClient(HttpClient client) : ITicketOfficeClient
{
    private readonly JsonSerializerOptions _options = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task NotifyFlightCreated(object dto)
        => await client.PostAsJsonAsync("flights", dto, _options);
}