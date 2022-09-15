using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client;
using MetricsManager.Services.Client.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class ManagerCpuMetricsController : ControllerBase
    {
		ILogger<ManagerCpuMetricsController> _logger;

        #region Services

        private IHttpClientFactory _httpClientFactory;
		private readonly AgentPool _agentPool;
        private IMetricsAgentClient _metricsAgentClient;

		#endregion

		public ManagerCpuMetricsController(
			IMetricsAgentClient metricsAgentClient,
			ILogger<ManagerCpuMetricsController> logger,
            AgentPool agentPool,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _agentPool = agentPool;
            _httpClientFactory = httpClientFactory;
            _metricsAgentClient = metricsAgentClient;
        }

		[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {

            var metrics = _metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });

            return Ok(metrics);
        }

        [HttpGet("agentOld/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgentOld(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("CPU GetMetricsFromAgent call");
			AgentInfoDto agentInfo = _agentPool.GetById(agentId);
            if (agentInfo == null)
                return BadRequest();

            string requestStr = $"{agentInfo.AgentUrl}api/metrics/cpu/from/" +
				$"{fromTime.ToString()}/to/{toTime.ToString()}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpClient httpClient = _httpClientFactory.CreateClient();
            CancellationTokenSource token = new CancellationTokenSource();
            token.CancelAfter(3000); // Таймаут ожидания ответа сервера 3 сек.
            HttpResponseMessage response = httpClient.Send(httpRequestMessage, token.Token);

            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse = 
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgenId = agentId;
                return Ok(cpuMetricsResponse);
            }
            
            return BadRequest();
        }

        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAll(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("CPU GetMetricsFromAll call");
			return Ok();
        }
    }
}
