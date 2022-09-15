using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Responses
{
    public class GetDotNetMetricResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
