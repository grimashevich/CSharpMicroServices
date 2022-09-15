using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models.Responses;
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
		private readonly IMapper _mapper;

		public AgentDonNetMetricsController(ILogger<AgentDonNetMetricsController> logger,
			IDotNetMetricsRepository dotNetMetricsRepository,
			IMapper mapper)
		{
			_logger = logger;
			_dotNetMetricsRepository = dotNetMetricsRepository;
			_mapper = mapper;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllCpuMetrics()
		{
			_logger.LogInformation("DotNet getall call");
			return Ok(_dotNetMetricsRepository.GetAll()
				.Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetCpuMetricById([FromRoute] int id)
		{
			_logger.LogInformation("DotNet getbyid call");
			return Ok(_mapper.Map<DotNetMetric>(_dotNetMetricsRepository.GetById(id)));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getDotNetMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			_logger.LogInformation("DotNet metrics call");
			GetDotNetMetricResponse response = new GetDotNetMetricResponse
			{
				Metrics = _dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime)
				.Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList()
			};
			return Ok(response);
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
		{
			_dotNetMetricsRepository.Create(_mapper.Map<DotNetMetric>(request));
			_logger.LogInformation("DotNet create metric call");
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateCpuMetric([FromBody] DotNetMetric request)
		{
			_logger.LogInformation("DotNet update metric call");
			_dotNetMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult DeleteCpuMetric([FromRoute] int id)
		{
			_logger.LogInformation("DotNet delete metric call");
			_dotNetMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
