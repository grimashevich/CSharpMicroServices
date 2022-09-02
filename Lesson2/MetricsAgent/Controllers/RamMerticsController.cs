using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
	[Route("api/metrics/ram")]
	[ApiController]
    public class RamMerticsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
		public IActionResult getRamMetrics([FromRoute] TimeSpan fromTime,
			[FromRoute] TimeSpan toTime)
		{
			return Ok();
		}
	}
}
