using MetricsAgent.Controllers;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
	public class DotNetControllerrTests
	{
		private DonNetMetricsController _donNetMetricsController;

		public DotNetControllerrTests()
		{
			_donNetMetricsController = new DonNetMetricsController();
		}

		[Fact]

		public void GetDotNetMetrics_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);
			var result = _donNetMetricsController.getDotNetMetrics(fromTime, toTime);
			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
