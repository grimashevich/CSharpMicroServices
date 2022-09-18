using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
	[Route("api/dotnet")]
	[ApiController]
	public class ManagerDotNetController : ControllerBase
	{
		ILogger<ManagerDotNetController> _logger;

		public ManagerDotNetController(ILogger<ManagerDotNetController> logger)
		{
			_logger = logger;
		}

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAgent(
			[FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("DotNet GetMetricsFromAgent call");
			return Ok();
		}

		[HttpGet("all/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAll(
			[FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("DotNet GetMetricsFromAll call");
			return Ok();
		}
	}
}
