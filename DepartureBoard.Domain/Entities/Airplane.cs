using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Airplane
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("baggage_available")]
    public int BaggageAvailable { get; set; }
    
    [JsonPropertyName("seats_available")]
    public int SeatsAvailable { get; set; }
    
    [JsonPropertyName("current_fuel")]
    public int CurrentFuel { get; set; }
    
    [JsonPropertyName("max_fuel")]
    public int MaxFuel { get; set; }
    
    public bool Handled { get; set; }
    
    [JsonIgnore]
    public Flight? Flight { get; set; }
}