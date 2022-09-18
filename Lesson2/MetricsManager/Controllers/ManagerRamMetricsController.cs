using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class ManagerRamMetricsController : ControllerBase
    {
		ILogger<ManagerRamMetricsController> _logger;

		#region Services

		private IHttpClientFactory _httpClientFactory;
		private readonly AgentPool _agentPool;
		private IMetricsAgentClient _metricsAgentClient;

		#endregion

		public ManagerRamMetricsController(
			IMetricsAgentClient metricsAgentClient,
			ILogger<ManagerRamMetricsController> logger,
			AgentPool agentPool,
			IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_agentPool = agentPool;
			_httpClientFactory = httpClientFactory;
			_metricsAgentClient = metricsAgentClient;
		}

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
		public ActionResult<RamMetricsResponse> GetMetricsFromAgent(
			[FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
			var metrics = _metricsAgentClient.GetRamMetrics(new RamMetricsRequest
			{
				AgentId = agentId,
				FromTime = fromTime,
				ToTime = toTime
			});
			return Ok(metrics);
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
