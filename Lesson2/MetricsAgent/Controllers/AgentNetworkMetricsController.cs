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
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentNetworkMetricsController : ControllerBase
    {
		private readonly ILogger<AgentNetworkMetricsController> _logger;
		private readonly INetworkMetricsRepository _networkMetricsRepository;
		private readonly IMapper _mapper;

		public AgentNetworkMetricsController(ILogger<AgentNetworkMetricsController> logger,
			INetworkMetricsRepository networkMetricsRepository,
			IMapper mapper)
        {
            _logger = logger;
            _networkMetricsRepository = networkMetricsRepository;
			_mapper = mapper;
		}

		#region Public methods

		[HttpGet("getall")]
		public IActionResult GetAllNetworkMetrics()
		{
			_logger.LogInformation("Network getall call");
			return Ok(_networkMetricsRepository.GetAll()
				.Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
		}

		[HttpGet("getbyid/{id}")]
		public IActionResult GetNetworkMetricById([FromRoute] int id)
		{
			_logger.LogInformation("Network getbyid call");
			return Ok(_mapper.Map<NetworkMetricDto>(_networkMetricsRepository.GetById(id)));
		}

		[HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult GetNetworkMetrics([FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Network metric call");
			return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)
				.Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
		{
			_logger.LogInformation("Network create metric call");
			_networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request));
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult Update([FromBody] NetworkMetric request)
		{
			_logger.LogInformation("Network update metric call");
			_networkMetricsRepository.Update(request);
			return Ok();
		}

		[HttpDelete("delete/{id}")]

		public IActionResult Delete([FromRoute] int id)
		{
			_logger.LogInformation("Network delete metric call");
			_networkMetricsRepository.Delete(id);
			return Ok();
		}

		#endregion
	}
}
