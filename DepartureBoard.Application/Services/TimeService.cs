using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.Services;

public class TimeService : IDisposable
{
    private long _ticks;
    private Timer? _timer;

    public int TicksPerSecond { get; set; } = 30;
    public DateTime Now => new DateTime(Interlocked.Read(ref _ticks));

    private bool _disposed;
    
    public void Run()
    {
        _ticks = DateTime.Now.Ticks;
        _timer = new Timer(UpdateTime, null, TimeSpan.Zero, 
            TimeSpan.FromMilliseconds((int)Constants.TickInMilliseconds));
    }

    private void UpdateTime(object? state)
    {
        Interlocked.Add(ref _ticks, TimeSpan.TicksPerSecond * TicksPerSecond);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _timer?.Dispose();
        }
        
        _disposed = true;
    }
    
    ~TimeService() => Dispose(false);
}