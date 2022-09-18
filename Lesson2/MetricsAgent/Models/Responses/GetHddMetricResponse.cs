using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Responses
{
    public class GetHddMetricResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
