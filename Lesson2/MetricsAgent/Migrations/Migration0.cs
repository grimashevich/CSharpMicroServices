using FluentMigrator;
using MetricsAgent.Models;

namespace MetricsAgent.Migrations
{
	[Migration(0)]
	public class Migration0 : Migration
	{
		string[] _tables = DbTables.GetTableNames().Values.ToArray();
		public override void Down()
		{
			foreach (var table in _tables)
			{
				Delete.Table(table);
			}
		}

		public override void Up()
		{
			foreach (var table in _tables)
			{
				Create.Table(table)
					.WithColumn("id").AsInt32().PrimaryKey().Identity()
					.WithColumn("value").AsInt32()
					.WithColumn("time").AsInt32();
			}
		}
	}
}
