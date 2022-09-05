using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/ram")]
	[ApiController]
    public class AgentRamMerticsController : ControllerBase
    {
		private readonly ILogger<AgentRamMerticsController> _logger;
		private readonly IRamMetricsRepository _ramMetricsRepository;

		public AgentRamMerticsController(ILogger<AgentRamMerticsController> logger,
			IRamMetricsRepository ramMetricsRepository)
		{
			_logger = logger;
			_ramMetricsRepository = ramMetricsRepository;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllCpuMetrics()
		{
			return Ok(_ramMetricsRepository.GetAll());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetCpuMetricById([FromRoute] int id)
		{
			return Ok(_ramMetricsRepository.GetById(id));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getRamMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("RAM metric call");
			return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime));
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] RamMetricCreateRequest request)
		{
			_ramMetricsRepository.Create(new Models.RamMetric
			{
				Value = request.Value,
				Time = (int)request.Time.TotalSeconds
			});
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateCpuMetric([FromBody] RamMetric request)
		{
			_ramMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult DeleteCpuMetric([FromRoute] int id)
		{
			_ramMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
