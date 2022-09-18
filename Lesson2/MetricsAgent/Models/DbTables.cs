namespace MetricsAgent.Models
{
	public static class DbTables
	{	
		public static Dictionary<string, string> GetTableNames()
		{
			var tableNames = new Dictionary<string, string>()
			{
				{"cpu", "cpumetrics"},
				{"ram", "rammetrics"},
				{"hdd", "hddmetrics"},
				{"net", "networkmetrics"},
				{".net", "dotnetmetrics"},
			};

			return tableNames;
		}

		public static string GetTableName(string name)
		{
			Dictionary<string, string> tables = GetTableNames();
			// Намеренно не проверяю на существование ключа,
			// чтобы в случае ошибки выпало исключение
			return tables[name];
		}
	}
}
