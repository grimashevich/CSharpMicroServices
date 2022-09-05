using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentNetworkMetricsController : ControllerBase
    {
		private readonly ILogger<AgentNetworkMetricsController> _logger;
		private readonly INetworkMetricsRepository _networkMetricsRepository;

        public AgentNetworkMetricsController(ILogger<AgentNetworkMetricsController> logger,
			INetworkMetricsRepository networkMetricsRepository)
        {
            _logger = logger;
            _networkMetricsRepository = networkMetricsRepository;
        }

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllCpuMetrics()
		{
			return Ok(_networkMetricsRepository.GetAll());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetCpuMetricById([FromRoute] int id)
		{
			return Ok(_networkMetricsRepository.GetById(id));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult GetNetworkMetrics([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Network metric call");
			return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime));
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
		{
			_networkMetricsRepository.Create(new Models.NetworkMetric
			{
				Value = request.Value,
				Time = (int)request.Time.TotalSeconds
			});
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateCpuMetric([FromBody] NetworkMetric request)
		{
			_networkMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult DeleteCpuMetric([FromRoute] int id)
		{
			_networkMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
