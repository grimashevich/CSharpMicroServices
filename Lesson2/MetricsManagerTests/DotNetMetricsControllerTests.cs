using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
	public class DotNetMetricsControllerTests
	{
		private DotNetController _dotNetController;

		public DotNetMetricsControllerTests()
		{
			_dotNetController = new DotNetController();
		}

		[Fact]
		public void GetMetricsFromAgent_ReturnOk()
		{
			int agentId = 1;
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);

			var result = _dotNetController.GetMetricsFromAgent(agentId, fromTime,
			toTime);

			Assert.IsAssignableFrom<IActionResult>(result);
		}

		[Fact]
		public void GetMetricsAll_ReturnOk()
		{
			TimeSpan fromTime = TimeSpan.FromSeconds(0);
			TimeSpan toTime = TimeSpan.FromSeconds(100);

			var result = _dotNetController.GetMetricsFromAll(fromTime, toTime);

			Assert.IsAssignableFrom<IActionResult>(result);
		}
	}
}
