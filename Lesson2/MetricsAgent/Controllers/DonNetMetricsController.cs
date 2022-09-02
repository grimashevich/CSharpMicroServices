using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/dotnet")]
	[ApiController]
	public class DonNetMetricsController : ControllerBase
	{
		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getDotNetMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			return Ok();
		}
	}
}
