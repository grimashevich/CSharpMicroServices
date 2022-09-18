using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Responses
{
    public class GetCpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
