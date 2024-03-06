namespace server.Services.Internal.Background;

public class SeatAssignmentBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer _timer;

    public SeatAssignmentBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    private async void HandleSeatAllocation(object state)
    {


    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Calculate time until next 16:00
        var now = DateTime.Now;
        var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 16, 0, 0);
        if (now > nextRunTime)
            nextRunTime = nextRunTime.AddDays(1);

        var dueTime = nextRunTime - now;

        _timer = new Timer(HandleSeatAllocation, null, dueTime, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}