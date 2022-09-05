using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class ManagerNetworkMetricsController : ControllerBase
    {
		ILogger<ManagerNetworkMetricsController> _logger;

		public ManagerNetworkMetricsController(ILogger<ManagerNetworkMetricsController> logger)
		{
			_logger = logger;
		}

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAgent(
			[FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("Network GetMetricsFromAgent call");
			return Ok();
		}

		[HttpGet("all/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAll(
			[FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("Network GetMetricsFromAll call");
			return Ok();
		}
	}
}
