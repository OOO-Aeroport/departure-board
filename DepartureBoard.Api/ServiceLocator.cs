using DepartureBoard.Application.Ports;

namespace DepartureBoard.Api;

public class ServiceLocator(IServiceProvider provider) : IServiceLocator
{
    private readonly IServiceProvider _provider = provider;
    
    public T? Require<T>()
        => _provider.GetService<T>();
}