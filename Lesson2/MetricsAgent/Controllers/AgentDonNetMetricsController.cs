using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/dotnet")]
	[ApiController]
	public class AgentDonNetMetricsController : ControllerBase
	{
		private readonly ILogger<AgentDonNetMetricsController> _logger;
		private readonly IDotNetMetricsRepository _dotNetMetricsRepository;

		public AgentDonNetMetricsController(ILogger<AgentDonNetMetricsController> logger,
			IDotNetMetricsRepository dotNetMetricsRepository)
		{
			_logger = logger;
			_dotNetMetricsRepository = dotNetMetricsRepository;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllCpuMetrics()
		{
			return Ok(_dotNetMetricsRepository.GetAll());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetCpuMetricById([FromRoute] int id)
		{
			return Ok(_dotNetMetricsRepository.GetById(id));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getDotNetMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("DotNet metrics call");
			return Ok(_dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime));
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
		{
			_dotNetMetricsRepository.Create(new Models.DotNetMetric
			{
				Value = request.Value,
				Time = (int)request.Time.TotalSeconds
			});
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateCpuMetric([FromBody] DotNetMetric request)
		{
			_dotNetMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult DeleteCpuMetric([FromRoute] int id)
		{
			_dotNetMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
