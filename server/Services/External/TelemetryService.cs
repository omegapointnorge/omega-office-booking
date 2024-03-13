using Microsoft.ApplicationInsights;
using Microsoft.Graph.Models.ExternalConnectors;

namespace server.Services.External
{
    public class TelemetryService : ITelemetryService
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties)
        {
            _telemetryClient.TrackEvent(eventName, properties);
        }


        public void TrackTrace(string message)
        {
            _telemetryClient.TrackTrace(message);
        }
    }
}
