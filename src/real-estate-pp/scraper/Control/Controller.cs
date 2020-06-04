using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RealEstates.Models;

namespace scraper
{
	public static class Controller
	{
		public static void Dump(List<RealEstate> estates)
		{
			Console.WriteLine($"{Environment.NewLine}\t\t=== Dumping Estates [{estates.Count}] ===");

			foreach (var estate in estates)
			{
				Console.WriteLine($"{Environment.NewLine}{estate.Description} [{estate.Price} EUR]");
				foreach (var prop in estate.Properties)
					Console.WriteLine($"\t{prop.Name}: {prop.Value}");
			}
		}

		public static async Task Run()
		{
			var crawler = new Crawler(1, 1);
			Console.Write("Running crawler . . .");
			var estates = await crawler.Run();
			Console.WriteLine(" Done!");
			Dump(estates);
		}
	}
}
