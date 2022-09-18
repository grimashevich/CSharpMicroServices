using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
	public class ManagerHddMetricsControllerTests
	{
		private ManagerHddMetricsController _hddMetricsController;

		public ManagerHddMetricsControllerTests()
		{
			_hddMetricsController = new ManagerHddMetricsController();
		}

		[Fact]
		public void GetMetricsFromAgent_ReturnOk()
		{
			int agentId = 1;
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);

			var result = _hddMetricsController.GetMetricsFromAgent(agentId, fromTime,
			toTime);

			Assert.IsAssignableFrom<IActionResult>(result);
		}

		[Fact]
		public void GetMetricsAll_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);

			var result = _hddMetricsController.GetMetricsFromAll(fromTime, toTime);

			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
