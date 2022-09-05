using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class ManagerCpuMetricsController : ControllerBase
    {
		ILogger<ManagerCpuMetricsController> _logger;

        public ManagerCpuMetricsController(ILogger<ManagerCpuMetricsController> logger)
        {
            _logger = logger;
        }

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("CPU GetMetricsFromAgent call");
            return Ok();
        }

        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAll(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("CPU GetMetricsFromAll call");
			return Ok();
        }
    }
}
