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
	[Route("api/metrics/hdd")]
	[ApiController]
    public class AgentHddMetricsController : ControllerBase
    {

		private readonly ILogger<AgentHddMetricsController> _logger;
		private readonly IHddMetricsRepository _hddMetricsRepository;
		private readonly IMapper _mapper;

		public AgentHddMetricsController(ILogger<AgentHddMetricsController> logger,
			IHddMetricsRepository hddMetricsRepository,
			IMapper mapper)
		{
			_logger = logger;
			_hddMetricsRepository = hddMetricsRepository;
			_mapper = mapper;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllHddMetrics()
		{
			_logger.LogInformation("Hdd getall call");
			return Ok(_hddMetricsRepository.GetAll()
				.Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetHddMetricById([FromRoute] int id)
		{
			_logger.LogInformation("Hdd getbyid call");
			return Ok(_mapper.Map<HddMetricDto>(_hddMetricsRepository.GetById(id)));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public ActionResult<IList<HddMetricDto>> GetHddMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("Hdd get by time period call");
			return Ok(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime)
				.Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList());
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] HddMetricCreateRequest request)
		{
			_logger.LogInformation("Hdd create metric call");
			_hddMetricsRepository.Create(_mapper.Map<HddMetric>(request));
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult Update([FromBody] HddMetric request)
		{
			_logger.LogInformation("Hdd update metric call");
			_hddMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult Delete([FromRoute] int id)
		{
			_logger.LogInformation("Hdd delete metric call");
			_hddMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
