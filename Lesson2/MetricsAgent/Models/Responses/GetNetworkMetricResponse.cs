using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Responses
{
    public class GetNetworkMetricResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
