using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
	public class DotNetMetricJob : IJob
	{
		private PerformanceCounter _dotNetCounter;
		private IServiceScopeFactory _serviceScopeFactory;

		public DotNetMetricJob(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			_dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all heaps", "_Global_");
		}

		public Task Execute(IJobExecutionContext context)
		{
			using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
			{
				var dotNetMetricsRepository = serviceScope.ServiceProvider.GetService<IDotNetMetricsRepository>();
				try
				{
					float dotNetUsageParcentage = _dotNetCounter.NextValue();
					int uts = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
					dotNetMetricsRepository.Create(new Models.DotNetMetric
					{
						Value = (int)dotNetUsageParcentage,
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
