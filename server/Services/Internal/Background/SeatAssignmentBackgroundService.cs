using Microsoft.ApplicationInsights;

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

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var todayPlusOneMonth = DateTime.Today.AddMonths(1);
            var serviceProvider = scope.ServiceProvider;
            var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

            try
            {
                var seatAllocationService = serviceProvider.GetRequiredService<ISeatAllocationService>();
                var bookingService = serviceProvider.GetRequiredService<IBookingService>();

                var seatAllocationDetails = await seatAllocationService.GetAllSeatAssignmentDetails();
                await bookingService.CreateBookingAsync(seatAllocationDetails, todayPlusOneMonth);

            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Calculate time until next 16:00
        var now = DateTime.Now;
        var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 16, 0, 0);

        if (now > nextRunTime)
            nextRunTime = nextRunTime.AddDays(1); // If it's already past 16:00, set it for the next day

        var dueTime = nextRunTime - now;


        _timer = new Timer(HandleSeatAllocation, null, dueTime, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }


    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}