using Dapper;
using MetricsAgent.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsManager.Models
{
    public class AgentPool
    {
		private readonly IOptions<DataBaseOptions> _dbOptions;
		private readonly string ConnectionString;

		public AgentPool(IOptions<DataBaseOptions> dbOptions)
        {
			_dbOptions = dbOptions;
			ConnectionString = _dbOptions.Value.ConnectionString;
		}

        public void Add(AgentInfo value)
        {
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
            connection.Execute($"INSERT INTO agents (agentid, agenturl, enabled) VALUES(@id, @url, @enabled)", new
            {
                id = value.AgentId,
                url = value.AgentUrl.ToString(),
                enabled = true
            });
		}
        public AgentInfo[]? GetAll()
        {
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
            var result = connection.Query<AgentInfoDto>
                ("SELECT agentid, agenturl, enabled FROM agents").ToArray();
            return AgentInfoConverter(result);
		}

        public AgentInfoDto? GetById(int id)
        {
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			var result = connection.QueryFirstOrDefault<AgentInfoDto> ($"SELECT agentid, agenturl, enabled FROM agents WHERE agentid = {id}");
			return result;
		}

        public AgentInfo? AgentInfoConverter(AgentInfoDto objIn)
        {
            if (objIn == null)
                return null;
            AgentInfo objOut = new AgentInfo {
                AgentId = objIn.AgentId,
                AgentUrl = new Uri(objIn.AgentUrl),
                Enable = objIn.Enable
            };
            return objOut;
        }

		public AgentInfo[]? AgentInfoConverter(AgentInfoDto[] objInArr)
		{
			if (objInArr == null)
				return null;
			AgentInfo[] objOutArr = new AgentInfo[objInArr.Length];
            for (int i = 0; i < objInArr.Length; i++)
            {
                objOutArr[i] = AgentInfoConverter(objInArr[i]);
            }
            return objOutArr;
		}
	}
}