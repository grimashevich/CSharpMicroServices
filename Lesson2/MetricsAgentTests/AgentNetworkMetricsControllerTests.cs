using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
	public class AgentNetworkMetricsControllerTests
	{
		private AgentNetworkMetricsController _networkMetricsController;

		public AgentNetworkMetricsControllerTests()
		{
			_networkMetricsController = new AgentNetworkMetricsController();
		}

		[Fact]
		public void GetRamMetrics_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);
			var result = _networkMetricsController.GetNetworkMetrics(fromTime, toTime);
			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
