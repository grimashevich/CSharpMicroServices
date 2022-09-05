using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
	public class AgentRamMetricsControllerTests
	{
		private AgentRamMerticsController _ramMetricsController;

		public AgentRamMetricsControllerTests()
		{
			_ramMetricsController = new AgentRamMerticsController();
		}

		[Fact]
		public void GetRamMetrics_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);
			var result = _ramMetricsController.getRamMetrics(fromTime, toTime);
			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
