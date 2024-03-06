using Microsoft.Graph;

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
            var serviceProvider = scope.ServiceProvider;
            var graphServiceClient = serviceProvider.GetRequiredService<GraphServiceClient>();

            try
            {
                var result = await graphServiceClient.Users.GetAsync(rc =>
                {
                    rc.QueryParameters.Filter = "mail eq 'fredrik.tornvall@omegapoint.no'";
                });

                var tmo = result;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing Microsoft Graph API: {ex.Message}");
            }
        }


    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Calculate time until next 16:00
        var now = DateTime.Now;
        var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 13, 29, 0);
        if (now > nextRunTime)
            nextRunTime = nextRunTime.AddDays(1);

        var dueTime = nextRunTime - now;
        dueTime = TimeSpan.FromSeconds(0);

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