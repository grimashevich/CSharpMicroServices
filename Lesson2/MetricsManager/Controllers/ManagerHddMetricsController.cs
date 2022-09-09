using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/hdd")]
    [ApiController]

    public class ManagerHddMetricsController : ControllerBase
    {
		ILogger<ManagerHddMetricsController> _logger;

		public ManagerHddMetricsController(ILogger<ManagerHddMetricsController> logger)
		{
			_logger = logger;
		}


		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAgent(
			[FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("HDD GetMetricsFromAgent call");
			return Ok();
		}

		[HttpGet("all/from/{fromTime}/to/{toTime}")]
		public IActionResult GetMetricsFromAll(
			[FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
            _logger.LogInformation("HDD GetMetricsFromAll call");
			return Ok();
		}
	}
}
