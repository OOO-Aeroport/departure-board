using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    public int AirplaneId { get; set; }
    public DateTime DepartureTime { get; set; }
    
    [JsonIgnore]
    public Airplane? Airplane { get; set; }
}