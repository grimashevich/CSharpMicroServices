using System.Text.Json.Serialization;

namespace MetricsManager.Models.Responses
{
    public class RamMetricsResponse
    {
        public int AgenId { get; set; }
        [JsonPropertyName("metrics")]
        public RamMetric[] Metrics { get; set; }
    }
}
