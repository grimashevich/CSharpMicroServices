using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Responses
{
    public class GetRamMetricResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
