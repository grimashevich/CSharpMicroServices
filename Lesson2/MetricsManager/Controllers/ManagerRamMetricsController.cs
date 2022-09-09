using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class ManagerRamMetricsController : ControllerBase
    {
		ILogger<ManagerRamMetricsController> _logger;

		public ManagerRamMetricsController(ILogger<ManagerRamMetricsController> logger)
		{
			_logger = logger;
		}

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAgent(
			[FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("RAM GetMetricsFromAgent call");
			return Ok();
		}

		[HttpGet("all/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAll(
			[FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("RAM GetMetricsFromAll call");
			return Ok();
		}
	}
}
