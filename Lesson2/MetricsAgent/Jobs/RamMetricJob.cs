using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
	public class RamMetricJob : IJob
	{
		private PerformanceCounter _ramCounter;
		private IServiceScopeFactory _serviceScopeFactory;

		public RamMetricJob(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_ramCounter = new PerformanceCounter("Memory", "Available MBytes");
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
			{
				var ramMetricsRepository = serviceScope.ServiceProvider.GetService<IRamMetricsRepository>();
				try
				{
					float ramUsageParcentage = _ramCounter.NextValue();
					int uts = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
					ramMetricsRepository.Create(new Models.RamMetric
					{
						Value = (int)ramUsageParcentage,
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
