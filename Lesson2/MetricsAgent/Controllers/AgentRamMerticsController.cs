using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
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
		private readonly IMapper _mapper;

		public AgentRamMerticsController(ILogger<AgentRamMerticsController> logger,
			IRamMetricsRepository ramMetricsRepository,
			IMapper mapper)
		{
			_logger = logger;
			_ramMetricsRepository = ramMetricsRepository;
			_mapper = mapper;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllRamMetrics()
		{
			_logger.LogInformation("Ram getall call");
			return Ok(_ramMetricsRepository.GetAll()
				.Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
		}

		[HttpGet("getbyid/{id}")]
		public ActionResult<RamMetricDto> GetRamMetricById([FromRoute] int id)
		{
			_logger.LogInformation("Ram getbyid call");
			return Ok(_mapper.Map<RamMetricDto>(_ramMetricsRepository.GetById(id)));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public ActionResult<IList<RamMetricDto>> GetRamMetrics(
			[FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("Ram get by time period call");
			return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
				.Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
		}
		[HttpPost("create")]
		public IActionResult Create([FromBody] RamMetricCreateRequest request)
		{
			_logger.LogInformation("Ram create metric call");
			_ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult Update([FromBody] RamMetric request)
		{
			_logger.LogInformation("Ram update metric call");
			_ramMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			_logger.LogInformation("Ram delete metric call");
			_ramMetricsRepository.Delete(id);
			return Ok();
		}
		#endregion
	}
}
