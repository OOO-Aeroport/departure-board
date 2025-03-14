namespace DepartureBoard.App.Ports.Network;

public interface IGroundHandlingClient
{
    Task Post(object dto);
}