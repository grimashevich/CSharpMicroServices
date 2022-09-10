using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
	public class NetworkMetricJob : IJob
	{
		private PerformanceCounter _networkCounter;
		private IServiceScopeFactory _serviceScopeFactory;

		public NetworkMetricJob(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_networkCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
			{
/*				var networkMetricsRepository = serviceScope.ServiceProvider.GetService<INetworkMetricsRepository>();
				try
				{
					float networkUsageParcentage = _networkCounter.NextValue();
					int uts = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
					networkMetricsRepository.Create(new Models.NetworkMetric
					{
						Value = (int)networkUsageParcentage,
						Time = uts
					});
				}
				catch
				{

				}
*/
				return Task.CompletedTask;
			}
		}
	}
}
