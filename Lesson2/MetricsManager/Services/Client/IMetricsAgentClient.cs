using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
    }
}
