using Microsoft.Extensions.Logging;

namespace DepartureBoard.App.Services;

public class TimeService(ILogger<TimeService> logger) : IDisposable
{
    private readonly ILogger<TimeService> _logger = logger;
    private long _ticks;
    private Timer? _timer;
    private DateTime _lastLoggedTime = DateTime.MinValue;
    public int SpeedFactor { get; set; } = 30;
    public DateTime Now => new DateTime(Interlocked.Read(ref _ticks));
    
    private bool _disposed;
    
    public Task Start()
    {
        _ticks = DateTime.Now.Ticks;
        _timer = new Timer(UpdateTime, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }

    private void UpdateTime(object? state)
    {
        Interlocked.Add(ref _ticks, TimeSpan.TicksPerSecond * SpeedFactor);

        var now = Now;
        if ((now - _lastLoggedTime).TotalHours < 1) return;
        _logger.LogInformation($"Time: {now}");
        _lastLoggedTime = now;
    }

    /*public void CountDown(int minutes, Action callback)
    {
        var endTime = Now.AddMinutes(minutes);
        var timer = new Timer(_ =>
        {
            if (Now >= endTime)
            {
                callback();
            }
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        timer.Dispose();
    }*/

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