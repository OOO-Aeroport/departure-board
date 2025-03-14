using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Flight
{
    public int Id { get; init; }
    public int AirplaneId { get; init; }
    public DateTime DepartureTime { get; init; }
    
    [JsonIgnore]
    public Airplane? Airplane { get; init; }
}