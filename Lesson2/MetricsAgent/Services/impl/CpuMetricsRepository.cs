using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
	public class CpuMetricsRepository : ICpuMetricsRepository
	{
		private readonly string ConnectionString;
		private readonly IOptions<DataBaseOptions> _dbOptions;
		private readonly string _dbTableName;

		public CpuMetricsRepository(IOptions<DataBaseOptions> dbOptions)
		{
			_dbOptions = dbOptions;
			ConnectionString = _dbOptions.Value.ConnectionString;
			_dbTableName = DbTables.GetTableName("cpu");
		}

		public void Create(CpuMetric item)
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

		public IList<CpuMetric> GetAll()
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return connection.Query<CpuMetric>($"SELECT * FROM {_dbTableName}").ToList();
		}

		public CpuMetric GetById(int id)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return connection.QuerySingle<CpuMetric>($"SELECT * FROM {_dbTableName} WHERE id={id}");
		}

		public IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			return (connection.Query<CpuMetric>($"SELECT * FROM {_dbTableName} WHERE " +
				$"time >= {timeFrom.TotalSeconds} AND time <= {timeTo.TotalSeconds}").ToList());
		}

		public void Update(CpuMetric item)
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
