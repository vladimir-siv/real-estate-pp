using System;

namespace scraper
{
	public static class Program
	{
		private static void Main(string[] args)
		{
			Tests.Run();

			Console.WriteLine();
			Console.WriteLine("Press any key to exit . . .");
			Console.ReadKey(true);
		}
	}
}
