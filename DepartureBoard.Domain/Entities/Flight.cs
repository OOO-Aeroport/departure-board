using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Flight
{
    public int Id { get; init; }
    public required int AirplaneId { get; init; }
    public required DateTime DepartureTime { get; init; }
    
    // Nav-prop
    [JsonIgnore]
    public Airplane? Airplane { get; init; }
}