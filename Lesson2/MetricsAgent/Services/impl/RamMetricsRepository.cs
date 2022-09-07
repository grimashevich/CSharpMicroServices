using Dapper;
using MetricsAgent.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
	public class RamMetricsRepository : IRamMetricsRepository
	{
		private readonly string ConnectionString;
		private readonly IOptions<DataBaseOptions> _dbOptions;
		private readonly string _dbTableName;

		public RamMetricsRepository(IOptions<DataBaseOptions> dbOptions)
		{
			_dbOptions = dbOptions;
			ConnectionString = _dbOptions.Value.ConnectionString;
			_dbTableName = DbTables.GetTableName("ram");
		}

		public void Create(RamMetric item)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			connection.Execute($"INSERT INTO {_dbTableName}(value, time) VALUES(@value, @time)", new
			{
				value = item.Value,
				time = item.Time
			});
		}

		public void Delete(int id)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			connection.Execute($"DELETE FROM {_dbTableName} WHERE id={id}");
		}

		public IList<RamMetric> GetAll()
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return connection.Query<RamMetric>($"SELECT * FROM {_dbTableName}").ToList();
		}

		public RamMetric GetById(int id)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return connection.QuerySingle<RamMetric>($"SELECT * FROM {_dbTableName} WHERE id={id}");
		}

		public IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return (connection.Query<RamMetric>($"SELECT * FROM {_dbTableName} WHERE " +
				$"time >= {timeFrom.TotalSeconds} AND time <= {timeTo.TotalSeconds}").ToList());
		}

		public void Update(RamMetric item)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			connection.Execute($"UPDATE {_dbTableName} SET value = @value, time = @time WHERE id = @id", new
			{
				value = item.Value,
				time = item.Time,
				id = item.Id
			});
		}
	}
}
