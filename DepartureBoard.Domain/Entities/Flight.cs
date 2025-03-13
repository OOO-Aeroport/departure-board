namespace DepartureBoard.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    public int AirplaneId { get; set; }
    public DateTime DepartureTime { get; set; }
    
    public Airplane Airplane { get; set; }
}