namespace server.Services.Internal.Background
{
    public class SeatAssignmentBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;

        public SeatAssignmentBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private async void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var seatAllocationService = serviceProvider.GetRequiredService<ISeatAllocationService>();
                var todayPlusOneMonth = DateTime.Today.AddMonths(1);

                var seatAssignments = await seatAllocationService.GetAllSeatAssignments();
                await seatAllocationService.GenerateSeatAssignmentBookings(seatAssignments, todayPlusOneMonth);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Calculate time until next 16:00
            var now = DateTime.Now;
            var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 14, 38, 0);
            if (now > nextRunTime)
                nextRunTime = nextRunTime.AddDays(1); // If it's already past 16:00, set it for the next day

            var dueTime = nextRunTime - now;

            _timer = new Timer(DoWork, null, dueTime, TimeSpan.FromDays(1)); // Run every day after the first run
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
}
