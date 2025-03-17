using System.Net.Http.Json;
using System.Text.Json;
using DepartureBoard.Application.Dto;
using DepartureBoard.Application.Ports.Network;

namespace DepartureBoard.Infrastructure.Network;

public class TicketOfficeHttpClient(HttpClient client) : ITicketOfficeClient
{
    private readonly HttpClient _client = client;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public async Task Post(FlightDto dto)
        => await _client.PostAsJsonAsync("ticket-office/flights", dto, _options);
}