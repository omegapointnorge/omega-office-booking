using Microsoft.ApplicationInsights;
using Microsoft.Graph.Models;
using server.Services.External;

namespace server.Services.Internal.Background;

public class SeatAssignmentBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer _timer1;
    private Timer _timer2;
    private IBookingService _bookingService;
    private readonly ITelemetryService _telemetryClient;

    public SeatAssignmentBackgroundService(IServiceScopeFactory serviceScopeFactory, IBookingService bookingService, ITelemetryService telemetryClient)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _bookingService = bookingService;
       _telemetryClient = telemetryClient;
}

    // check if he/she has never any booking for the next month
    private async void InitiateSeatAllocation(object state)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var todayPlusOneMonth = DateTime.Today.AddDays(60).AddHours(12);
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var seatAllocationService = serviceProvider.GetRequiredService<ISeatAllocationService>();
                

                var seatAllocationDetails = await seatAllocationService.GetAllSeatAssignmentDetails();
                //TODO: here should implemente RecurringBooking for the next 29 days
                await _bookingService.CreateRecurringBookingAsync(seatAllocationDetails, todayPlusOneMonth);

            }
            catch (Exception ex)
            {
                _telemetryClient.TrackTrace(ex.ToString());
            }
        }
    }

    private async void HandleSeatAllocation(object state)
    {

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var todayPlusOneMonth = DateTime.Today.AddDays(30).AddHours(12);
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var seatAllocationService = serviceProvider.GetRequiredService<ISeatAllocationService>();

                var seatAllocationDetails = await seatAllocationService.GetAllSeatAssignmentDetails();
                await _bookingService.CreateRecurringBookingAsync(seatAllocationDetails, todayPlusOneMonth);

            }
            catch (Exception ex)
            {
                _telemetryClient.TrackTrace(ex.ToString());
            }
        }
    }

   

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Calculate time until next 17:00
        var now = DateTime.Now;
        var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 17, 0, 0);

        if (now > nextRunTime)
            nextRunTime = nextRunTime.AddDays(1); // If it's already past 17:00, set it for the next day

        var dueTime = nextRunTime - now;


        _timer1 = new Timer(HandleSeatAllocation, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        _timer2 = new Timer(InitiateSeatAllocation, null, TimeSpan.Zero, Timeout.InfiniteTimeSpan);

        return Task.CompletedTask;
    }


    public override void Dispose()
    {
        _timer1?.Dispose();
        _timer2?.Dispose();
        base.Dispose();
    }
}