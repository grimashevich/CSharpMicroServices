using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/hdd")]
	[ApiController]
    public class AgentHddMetricsController : ControllerBase
    {

		private readonly ILogger<AgentHddMetricsController> _logger;
		private readonly IHddMetricsRepository _hddMetricsRepository;

		public AgentHddMetricsController(ILogger<AgentHddMetricsController> logger,
			IHddMetricsRepository hddMetricsRepository)
		{
			_logger = logger;
			_hddMetricsRepository = hddMetricsRepository;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllCpuMetrics()
		{
			return Ok(_hddMetricsRepository.GetAll());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetCpuMetricById([FromRoute] int id)
		{
			return Ok(_hddMetricsRepository.GetById(id));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getHddMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("HDD merics call");
			return Ok(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime));
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] HddMetricCreateRequest request)
		{
			_hddMetricsRepository.Create(new Models.HddMetric
			{
				Value = request.Value,
				Time = (int)request.Time.TotalSeconds
			});
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateCpuMetric([FromBody] HddMetric request)
		{
			_hddMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult DeleteCpuMetric([FromRoute] int id)
		{
			_hddMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
