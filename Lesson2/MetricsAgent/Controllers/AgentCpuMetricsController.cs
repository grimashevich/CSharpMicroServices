using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class AgentCpuMetricsController : ControllerBase
    {

        private readonly ILogger<AgentCpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        public AgentCpuMetricsController(ILogger<AgentCpuMetricsController> logger,
			ICpuMetricsRepository cpuMetricsRepository,
            IMapper mapper)
        {
            _logger = logger;
            _cpuMetricsRepository = cpuMetricsRepository;
            _mapper = mapper;
        }

		#region Public methods

        [HttpGet("getall")]
        public IActionResult GetAllCpuMetrics()
        {
			_logger.LogInformation("Cpu getall call");
			return Ok(_cpuMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
        }

		[HttpGet("getbyid/{id}")]
        public ActionResult<CpuMetricDto> GetCpuMetricById([FromRoute] int id)
        {
			_logger.LogInformation("Cpu getbyid call");
			return Ok(_mapper.Map<CpuMetricDto>(_cpuMetricsRepository.GetById(id)));
        }

		[HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Cpu get by time period call");
			return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
		}
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
			_logger.LogInformation("Cpu create metric call");
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] CpuMetric request)
        {
			_logger.LogInformation("Cpu update metric call");
			_cpuMetricsRepository.Update(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
			_logger.LogInformation("Cpu delete metric call");
			_cpuMetricsRepository.Delete(id);
            return Ok();
        }
        #endregion
    }
}
