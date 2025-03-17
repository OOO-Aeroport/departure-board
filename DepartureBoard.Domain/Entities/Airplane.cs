using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Airplane
{
    public required int Id { get; init; }
    public required int BaggageAvailable { get; init; }
    public required int SeatsAvailable { get; init; }
    public required int CurrentFuel { get; init; }
    public required int MaxFuel { get; init; }
    
    // Nav-prop
    [JsonIgnore]
    public Flight? Flight { get; init; }
}