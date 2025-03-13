namespace DepartureBoard.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    public int PlaneId { get; set; }
    public int PassengersCount { get; set; }
    
    public Airplane Airplane { get; set; }
}