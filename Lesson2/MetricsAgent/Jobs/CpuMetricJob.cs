using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
	public class CpuMetricJob : IJob
	{
		private PerformanceCounter _cpuCounter;
		private IServiceScopeFactory _serviceScopeFactory;

		public CpuMetricJob(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
			{
				var cpuMetricsRepository = serviceScope.ServiceProvider.GetService<ICpuMetricsRepository>();
				try
				{
					float cpuUsageParcentage = _cpuCounter.NextValue();
					int uts = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
					cpuMetricsRepository.Create(new Models.CpuMetric
					{
						Value = (int)cpuUsageParcentage,
						Time = uts
					});
				}
				catch
				{

				}
				return Task.CompletedTask;
			}
		}
	}
}
