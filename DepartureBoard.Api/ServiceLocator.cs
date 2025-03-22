using DepartureBoard.Application.Ports;

namespace DepartureBoard.Api;

public class ServiceLocator(IServiceProvider provider) : IServiceLocator
{
    public T? Require<T>()
        => provider.GetService<T>();
}