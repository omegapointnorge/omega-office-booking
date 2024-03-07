using Microsoft.ApplicationInsights;
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
            var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

            try
            {
                string email = "nils.olav.johansen@omegapoint.no";
                var result = await graphServiceClient.Users.GetAsync(rc =>
                {
                    rc.QueryParameters.Filter = $"mail eq '{email}'";
                });

                string logMessage = result?.Value != null && result.Value.Any()
    ? $"From {email} found {result.Value[0].DisplayName} with id {result.Value[0].Id}"
    : $"No user found with email {email}.";

                var eventData = new Dictionary<string, string>
            {
                { "Background", logMessage }
            };

                telemetryClient.TrackEvent("Background", eventData);
            }
            catch (Exception ex)
            {
                var logMessage = "Error accessing Microsoft Graph API";
                var eventData = new Dictionary<string, string>
            {
                { "Background", logMessage }
            };

                telemetryClient.TrackEvent("Background", eventData);

                Console.WriteLine($"Error accessing Microsoft Graph API: {ex.Message}");
            }
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //// Calculate time until next 16:00
        //var now = DateTime.Now;
        //var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 13, 29, 0);
        //if (now > nextRunTime)
        //    nextRunTime = nextRunTime.AddDays(1);

        //var dueTime = nextRunTime - now;
        var dueTime = TimeSpan.FromSeconds(0);

        //_timer = new Timer(HandleSeatAllocation, null, dueTime, TimeSpan.FromDays(1));
        _timer = new Timer(HandleSeatAllocation, null, dueTime, TimeSpan.FromMinutes(5));

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