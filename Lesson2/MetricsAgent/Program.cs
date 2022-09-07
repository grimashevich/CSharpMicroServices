using MetricsAgent.Converters;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure options

			builder.Services.Configure<DataBaseOptions>(options =>
			{
				builder.Configuration.GetSection("Settings:DataBase").Bind(options);
			});

			#endregion

			#region Configure logging

			builder.Host.ConfigureLogging(logging =>
			{
				logging.ClearProviders();
				logging.AddConsole();

			}).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

			builder.Services.AddHttpLogging(logging =>
			{
				logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
				logging.RequestBodyLogLimit = 4096;
				logging.ResponseBodyLogLimit = 4096;
				logging.RequestHeaders.Add("Authorization");
				logging.RequestHeaders.Add("X-Real-IP");
				logging.RequestHeaders.Add("X-Forwarded-For");
			});

			#endregion

			#region Configure repository

			builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
			builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
			builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
			builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
			builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();

			ConfigureSqlLiteConnection(builder);

			#endregion

			builder.Services.AddControllers()
			  .AddJsonOptions(options =>
				  options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));
		
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

				// ��������� TimeSpan
				c.MapType<TimeSpan>(() => new OpenApiSchema
				{
					Type = "string",
					Example = new OpenApiString("00:00:00")
				});
			});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
			app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }

		private static void ConfigureSqlLiteConnection(WebApplicationBuilder? builder)
		{
			var connection = new SQLiteConnection(builder.Configuration["Settings:DataBase:ConnectionString"]);
			connection.Open();
			PrepareSchema(connection);
		}

		private static void PrepareSchema(SQLiteConnection connection)
		{
			CreateTablesIfNotExist(DbTables.GetTableNames().Values.ToArray(), connection);
		}

		private static void CreateTablesIfNotExist(string[] tableNames, SQLiteConnection connection)
		{

			using (var command = new SQLiteCommand(connection))
			{
				foreach (var tableName in tableNames)
				{
					command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}";
					command.CommandText = command.CommandText +  
						"(id INTEGER PRIMARY KEY, value INT, time INT)";
					command.ExecuteNonQuery();
				}
			}

		}
	}
}