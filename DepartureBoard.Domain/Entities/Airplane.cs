using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Airplane
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    [JsonPropertyName("baggage_available")]
    public int BaggageAvailable { get; init; }
    
    [JsonPropertyName("seats_available")]
    public int SeatsAvailable { get; init; }
    
    [JsonPropertyName("current_fuel")]
    public int CurrentFuel { get; init; }
    
    [JsonPropertyName("max_fuel")]
    public int MaxFuel { get; init; }
    
    public bool Handled { get; set; }
    
    [JsonIgnore]
    public Flight? Flight { get; init; }
    
}