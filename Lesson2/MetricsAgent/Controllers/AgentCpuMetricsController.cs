using MetricsAgent.Models;
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

        public AgentCpuMetricsController(ILogger<AgentCpuMetricsController> logger,
			ICpuMetricsRepository cpuMetricsRepository)
        {
            _logger = logger;
            _cpuMetricsRepository = cpuMetricsRepository;
        }

		#region Public methods

        [HttpGet("getall")]
        public IActionResult GetAllCpuMetrics()
        {
            return Ok(_cpuMetricsRepository.GetAll());
        }

		[HttpGet("getbyid/{id}")]
        public IActionResult GetCpuMetricById([FromRoute] int id)
        {
            return Ok(_cpuMetricsRepository.GetById(id));
        }

		[HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {

            _logger.LogInformation("Cpu metrics call");
			return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime));
		}

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _cpuMetricsRepository.Create(new Models.CpuMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult UpdateCpuMetric([FromBody] CpuMetric request)
        {
            _cpuMetricsRepository.Update(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]

        public IActionResult DeleteCpuMetric([FromRoute] int id)
        {
            _cpuMetricsRepository.Delete(id);
            return Ok();
        }

        #endregion

    }
}
