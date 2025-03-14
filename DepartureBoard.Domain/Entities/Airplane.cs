using System.Text.Json.Serialization;

namespace DepartureBoard.Domain.Entities;

public class Airplane
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("gate")]
    public int Gate { get; set; }
    
    [JsonPropertyName("baggageAvailable")]
    public int BaggageAvailable { get; set; }
    
    [JsonPropertyName("seatsAvailable")]
    public int SeatsAvailable { get; set; }
    
    [JsonPropertyName("currentFuel")]
    public int CurrentFuel { get; set; }
    
    [JsonPropertyName("maxFuel")]
    public int MaxFuel { get; set; }
    
    public bool Handled { get; set; }
    
    public Flight Flight { get; set; }
}