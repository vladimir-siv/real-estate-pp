using System;
using System.Threading.Tasks;

namespace analyzer
{
	public static class Controller
	{
		public static async Task Run()
		{
			Console.WriteLine("Running database analysis . . .");

			Console.WriteLine();
			Console.WriteLine($"Count of Sales: {Queries.CountOfSales()}");
			Console.WriteLine($"Count of Rents: {Queries.CountOfRents()}");

			Console.WriteLine();
			Console.WriteLine("Count of RealEstates per City:");
			foreach (var city in Queries.CountOfSalesPerCity()) Console.WriteLine($"\t{city.Item1}: {city.Item2}");

			Console.WriteLine();
			Console.WriteLine("House registrations:");
			foreach (var reg in Queries.CountOfRegistrations(Category.House)) Console.WriteLine($"\t{reg.Item1}: {reg.Item2}");
			Console.WriteLine("Appartment registrations:");
			foreach (var reg in Queries.CountOfRegistrations(Category.Appartment)) Console.WriteLine($"\t{reg.Item1}: {reg.Item2}");

			Console.WriteLine();
			Console.WriteLine("Top 20 Houses in Serbia:");
			foreach (var re in Queries.TopSaleList(Category.House, 20, "Srbija")) Console.WriteLine($"\t{re.Item1}: {re.Item2}");
			Console.WriteLine("Top 20 Appartments in Serbia:");
			foreach (var re in Queries.TopSaleList(Category.Appartment, 20, "Srbija")) Console.WriteLine($"\t{re.Item1}: {re.Item2}");

			Console.WriteLine();
			Console.WriteLine("Top 100 Houses for Rent:");
			foreach (var re in Queries.TopRentList(Category.House, 100)) Console.WriteLine($"\t{re.Item1}: {re.Item2 ?? 0}");
			Console.WriteLine("Top 100 Appartments for Rent:");
			foreach (var re in Queries.TopRentList(Category.Appartment, 100)) Console.WriteLine($"\t{re.Item1}: {re.Item2 ?? 0}");

			Console.WriteLine();
			Console.WriteLine("Top Real Estate List in 2019:");
			foreach (var re in Queries.TopListByYear(2019)) Console.WriteLine($"\t{re.Item1}: {re.Item2 ?? 0}");

			var eroom = Queries.BestRealEstateByProperty("Ukupan broj soba");
			var ebath = Queries.BestRealEstateByProperty("Broj kupatila");
			var egrnd = Queries.BestRealEstateByProperty("Površina zemljišta", s => Convert.ToDecimal(s.Split(' ')[0]));

			Console.WriteLine();
			Console.WriteLine($"Real Estate with most rooms\t[{eroom.Item2}]: {eroom.Item1}");
			Console.WriteLine($"Real Estate with most bathrooms\t[{ebath.Item2}]: {ebath.Item1}");
			Console.WriteLine($"Real Estate with most ground\t[{egrnd.Item2}]: {egrnd.Item1}");
		}
	}
}
