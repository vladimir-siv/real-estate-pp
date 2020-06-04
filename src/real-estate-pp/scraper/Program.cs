using System;

namespace scraper
{
	public static class Program
	{
		private static void Main(string[] args)
		{
			App.Init();

			try
			{
				Controller.Run().Wait();
			}
			catch (Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine(ex.GetType().FullName);
				Console.WriteLine();
				Console.WriteLine(ex.Message);

				if (ex.InnerException != null)
				{
					Console.WriteLine();
					Console.Write("Inner exception: ");
					Console.WriteLine(ex.InnerException.GetType().FullName);
					Console.WriteLine();
					Console.WriteLine(ex.InnerException.Message);
				}
			}

			App.Dispose();
			Console.WriteLine($"{Environment.NewLine}Press any key to exit . . .");
			Console.ReadKey(true);
		}
	}
}
