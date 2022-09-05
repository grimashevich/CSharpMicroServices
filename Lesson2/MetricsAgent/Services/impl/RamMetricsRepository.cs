using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
	public class RamMetricsRepository : IRamMetricsRepository
	{
		private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

		private readonly string _dbTableName;

		public RamMetricsRepository()
		{
			_dbTableName = DbTables.GetTableName("ram");
		}

		public void Create(RamMetric item)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = $"INSERT INTO {_dbTableName}(value, time) VALUES(@value, @time)";
			cmd.Parameters.AddWithValue("@value", item.Value);
			cmd.Parameters.AddWithValue("@time", item.Time);
			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}

		public void Delete(int id)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = $"DELETE FROM {_dbTableName} WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}

		public IList<RamMetric> GetAll()
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = $"SELECT * FROM {_dbTableName}";
			var returnList = new List<RamMetric>();
			using (SQLiteDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					returnList.Add(new RamMetric
					{
						Id = reader.GetInt32(0),
						Value = reader.GetInt32(1),
						Time = reader.GetInt32(2)
					});
				}
			}
			return returnList;
		}

		public RamMetric GetById(int id)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = $"SELECT * FROM {_dbTableName} WHERE id={id}";
			using (SQLiteDataReader reader = cmd.ExecuteReader())
			{
				if (reader.Read())
				{
					return new RamMetric
					{
						Id = reader.GetInt32(0),
						Value = reader.GetInt32(1),
						Time = reader.GetInt32(2)
					};
				}
				return null;
			}
		}

		public IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			connection.Open();
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = $"SELECT * FROM {_dbTableName} where time >= @timeFrom and time <= @timeTo";
			cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
			cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
			var returnList = new List<RamMetric>();
			using (SQLiteDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					returnList.Add(new RamMetric
					{
						Id = reader.GetInt32(0),
						Value = reader.GetInt32(1),
						Time = reader.GetInt32(2)
					});
				}
			}
			return returnList;
		}

		public void Update(RamMetric item)
		{
			using var connection = new SQLiteConnection(ConnectionString);
			using var cmd = new SQLiteCommand(connection);
			connection.Open();
			cmd.CommandText = $"UPDATE {_dbTableName} SET value = @value, time = @time WHERE id = @id; ";
			cmd.Parameters.AddWithValue("@id", item.Id);
			cmd.Parameters.AddWithValue("@value", item.Value);
			cmd.Parameters.AddWithValue("@time", item.Time);
			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}
	}
}
