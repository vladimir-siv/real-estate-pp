using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RealEstates.Models;

using dbdriver;

namespace scraper
{
	public static class Controller
	{
		private static void Dump(List<RealEstate> estates)
		{
			Console.WriteLine($"{Environment.NewLine}\t\t=== Dumping Estates [{estates.Count}] ===");

			foreach (var estate in estates)
			{
				Console.WriteLine($"{Environment.NewLine}{estate.Description} [{estate.Price} EUR]");
				foreach (var prop in estate.Properties)
					Console.WriteLine($"\t{prop.Name}: {prop.Value}");
			}
		}

		private static void DeleteDatabase()
		{
			var db = AppContext.Resolve<RealEstateModel>();
			db.Database.Delete();
		}

		private static void PageCompleted(int page, IReadOnlyList<RealEstate> estates)
		{
			var db = AppContext.Resolve<RealEstateModel>();
			foreach (var estate in estates) db.RealEstates.Add(estate);
			db.SaveChanges();
			Console.WriteLine($"Page {page} completed.");
		}

		private static void PageError(int page, Exception ex)
		{
			Console.WriteLine($"An error occured on page {page}.");
			ex.Dump();
		}

		public static async Task Run()
		{
			var crawler = new Crawler(101, 200);
			crawler.PageCompleted += PageCompleted;
			crawler.PageError += PageError;
			Console.WriteLine("Running crawler . . .");
			await crawler.Run();
			Console.WriteLine("Crawler done!");
		}
	}
}
