using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Network.Factories;

namespace DepartureBoard.Api.Factories;

public class CheckInClientFactory(IServiceProvider provider) : ICheckInClientFactory
{
    private readonly IServiceProvider _provider = provider;
    
    public ICheckInClient? Require()
        => _provider.GetService<ICheckInClient>();
}