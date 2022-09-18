using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace MetricsManager.Services.Client.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {

        #region Services

        private readonly AgentPool _agentPool;
        private readonly HttpClient _httpClient;

        #endregion

        public MetricsAgentClient(HttpClient httpClient,
            AgentPool agentPool)
        {
            _httpClient = httpClient;
            _agentPool = agentPool;
        }


        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.AgentInfoConverter(_agentPool.GetById(request.AgentId));
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentUrl}api/metrics/cpu/from/{request.FromTime.ToString()}" +
				$"/to/{request.ToTime.ToString()}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgenId = request.AgentId;
                return cpuMetricsResponse;
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request)
        {
			AgentInfo agentInfo = _agentPool.AgentInfoConverter(_agentPool.GetById(request.AgentId));
			if (agentInfo == null)
				return null;

			string requestStr =
				$"{agentInfo.AgentUrl}api/metrics/ram/from/{request.FromTime.ToString()}" +
				$"/to/{request.ToTime.ToString()}";
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
			httpRequestMessage.Headers.Add("Accept", "application/json");
			HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
			if (response.IsSuccessStatusCode)
			{
				string responseStr = response.Content.ReadAsStringAsync().Result;
				RamMetricsResponse ramMetricsResponse =
					(RamMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RamMetricsResponse));
				ramMetricsResponse.AgenId = request.AgentId;
				return ramMetricsResponse;
			}
			return null;
		}
    }
}
