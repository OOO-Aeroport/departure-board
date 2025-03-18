namespace DepartureBoard.Application.Ports;

public interface IServiceLocator
{
    T? Require<T>();
}