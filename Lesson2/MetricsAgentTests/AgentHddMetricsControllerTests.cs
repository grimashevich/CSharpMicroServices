using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
	public class AgentHddMetricsControllerTests
	{
		private AgentHddMetricsController _hddMetricsController;

		public AgentHddMetricsControllerTests()
		{
			_hddMetricsController = new AgentHddMetricsController();
		}

		[Fact]
		public void GetHddMetrics_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);
			var result = _hddMetricsController.getHddMetrics(fromTime, toTime);
			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
