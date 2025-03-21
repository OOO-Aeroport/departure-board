using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Airplane
{
    public required int Id { get; init; }
    [JsonPropertyName("baggage_available")]
    public required int BaggageAvailable { get; init; }
    [JsonPropertyName("seats_available")]
    public required int SeatsAvailable { get; init; }
    
    // Nav-prop
    [JsonIgnore]
    public Flight? Flight { get; init; }
}