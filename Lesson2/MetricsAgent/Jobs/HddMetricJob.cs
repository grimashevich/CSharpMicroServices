using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
	public class HddMetricJob : IJob
	{
		private PerformanceCounter _hddCounter;
		private IServiceScopeFactory _serviceScopeFactory;

		public HddMetricJob(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
			{
				var hddMetricsRepository = serviceScope.ServiceProvider.GetService<IHddMetricsRepository>();
				try
				{
					float hddUsageParcentage = _hddCounter.NextValue();
					int uts = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
					hddMetricsRepository.Create(new Models.HddMetric
					{
						Value = (int)hddUsageParcentage,
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
