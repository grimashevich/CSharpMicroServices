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
			Delete.Table("agents");
		}

		public override void Up()
		{
			foreach (var table in _tables)
			{
				Create.Table(table)
					.WithColumn("id").AsInt32().PrimaryKey().Identity()
					.WithColumn("agentid").AsInt32()
					.WithColumn("value").AsInt32()
					.WithColumn("time").AsInt32();
			}
			Create.Table("agents")
				.WithColumn("id").AsInt32().PrimaryKey().Identity()
				.WithColumn("agentid").AsInt32()
				.WithColumn("agenturl").AsFixedLengthString(2048)
				.WithColumn("enabled").AsBoolean();
		}
	}
}
