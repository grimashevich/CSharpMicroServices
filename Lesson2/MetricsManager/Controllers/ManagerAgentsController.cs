using MetricsAgent.Models;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerAgentsController : ControllerBase
    {
		#region Services

		ILogger<ManagerAgentsController> _logger;
		private readonly IOptions<DataBaseOptions> _dbOptions;
		private readonly AgentPool _agentPool;

		#endregion

        #region Constuctors
        public ManagerAgentsController(ILogger<ManagerAgentsController> logger,
            AgentPool agentPool,
			IOptions<DataBaseOptions> dbOptions)
        {
            _logger = logger;
			_agentPool = agentPool;
		}
        
        #endregion


        #region Public Methods

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("RegisterAgent call");
            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
            }
            return Ok();
        }
/*
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("EnableAgentById call");
            if (_agentPool.Agents.ContainsKey(agentId))
                _agentPool.Agents[agentId].Enable = true;
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
			_logger.LogInformation("DisableAgentById call");
			if (_agentPool.Agents.ContainsKey(agentId))
                _agentPool.Agents[agentId].Enable = false;
            return Ok();
        }*/
        
        [HttpGet("getall")]
        public ActionResult<AgentInfo[]> GetAllAgents()
        {
			_logger.LogInformation("GetAllAgents call");
			return Ok(_agentPool.GetAll());
        }

        [HttpGet("getbyid/{id}")]
        public ActionResult<AgentInfo> GetAgentById([FromRoute] int id)
        {
			_logger.LogInformation("GetAllAgents call");
			return Ok(_agentPool.GetById(id));
        }

        #endregion

    }
}
