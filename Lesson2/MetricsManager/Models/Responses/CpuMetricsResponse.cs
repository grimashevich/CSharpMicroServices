using System.Text.Json.Serialization;

namespace MetricsManager.Models.Responses
{
    public class CpuMetricsResponse
    {
        public int AgenId { get; set; }
        [JsonPropertyName("metrics")]
        public CpuMetric[] Metrics { get; set; }
    }
}
