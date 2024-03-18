namespace server.Services.External
{
    public interface ITelemetryService
    {
        public void TrackEvent(string eventName, IDictionary<string, string> properties);
        public void TrackTrace(string message);
    }
}
