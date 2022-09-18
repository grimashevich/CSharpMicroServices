namespace MetricsManager.Client
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			ManagerAgentsClient agentsClient = 
				new ManagerAgentsClient("http://localhost:5159/", new HttpClient());
			ManagerCpuMetricsClient cpuMetricsClient = 
				new ManagerCpuMetricsClient("http://localhost:5159/", new HttpClient());

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Задачи");
				Console.WriteLine("==============================================");
				Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
				Console.WriteLine("0 - Завершение работы приложения");
				Console.WriteLine("==============================================");
				Console.Write("Введите номер задачи: ");
				if (int.TryParse(Console.ReadLine(), out int taskNumber))
				{
					switch (taskNumber)
					{
						case 0:
							Console.WriteLine("Завершение работы приложения.");
							Console.ReadKey(true);
							break;

						case 1:
							try
							{
								TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
								TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
								CpuMetricsResponse response = await cpuMetricsClient.ToGetAsync(
									3,
									fromTime.ToString(),
									toTime.ToString());
								foreach (CpuMetric metric in response.Metrics)
									Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString()} >>> {metric.Value}");
							}
							catch (Exception e)
							{
								Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
							}
							Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
							Console.ReadKey(true);
							break;

						default:
							Console.WriteLine("Введите корректный номер подзадачи.");
							break;
					}
				}
			}
		}

	}
} 