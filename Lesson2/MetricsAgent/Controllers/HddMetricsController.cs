﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/hdd")]
	[ApiController]
    public class HddMetricsController : ControllerBase
    {
		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getHddMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			return Ok();
		}
	}
}
