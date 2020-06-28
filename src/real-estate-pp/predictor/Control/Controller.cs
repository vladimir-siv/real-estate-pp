using System;
using System.Threading.Tasks;

using repplib;

namespace predictor
{
	public static class Controller
	{
		private static void LinearRegression()
		{
			Console.WriteLine("Fetching data . . .");
			var data = Queries.GetData();

			Console.WriteLine("Preprocessing data for linear regression . . .");
			var train = (int)(data.Count * 0.9);
			var lr = new LinearRegressor(5);
			for (var i = 0; i < train; ++i)
			{
				var x = new double[5]
				{
					Location.ToDistance(data[i].Get("Deo Grada")),
					Convert.ToDouble(data[i].Get("Kvadratura")?.Split(' ')[0] ?? "0") / 100000,
					Convert.ToDouble(data[i].Get("Godina izgradnje") ?? "0") / 2020,
					Convert.ToDouble(data[i].Get("Ukupan broj soba") ?? "0") / 1000,
					Convert.ToDouble(data[i].Get("Ukupan broj spratova") ?? "0") / 500
				};

				lr.Add(x, Convert.ToDouble(data[i].Price));
			}

			Console.WriteLine("Training linear regressor . . .");
			lr.Train(LinearRegressor.MSE, 1e-6, 10000);

			Console.WriteLine("Test\t\tTrue\t\t\tPredicted");
			for (var i = train; i < data.Count; ++i)
			{
				var x = new double[5]
				{
					Location.ToDistance(data[i].Get("Deo Grada")),
					Convert.ToDouble(data[i].Get("Kvadratura")?.Split(' ')[0] ?? "0") / 100000,
					Convert.ToDouble(data[i].Get("Godina izgradnje") ?? "0") / 2020,
					Convert.ToDouble(data[i].Get("Ukupan broj soba") ?? "0") / 1000,
					Convert.ToDouble(data[i].Get("Ukupan broj spratova") ?? "0") / 500
				};

				var yh = lr.Predict(x);

				Console.WriteLine($"{i - train + 1}:\t\t{data[i].Price:F2}\t\t{yh:F2}");
			}
		}
		private static void NearestNeighbors()
		{
			var k = 0;

			Console.Write("K resolution: [A]utomatic/[M]anual: ");
			if (Console.ReadLine().ToLowerInvariant()[0] == 'm')
			{
				Console.Write("K: ");
				k = Convert.ToInt32(Console.ReadLine());
			}
			
			Console.WriteLine("Fetching data . . .");
			var data = Queries.GetData();

			Console.WriteLine("Preprocessing data for kNN . . .");
			var train = (int)(data.Count * 0.9);
			var knn = new kNN { DistanceFunc = kNN.L2 };
			for (var i = 0; i < train; ++i)
			{
				var x = new double[5]
				{
					Location.ToDistance(data[i].Get("Deo Grada")),
					Convert.ToDouble(data[i].Get("Kvadratura")?.Split(' ')[0] ?? "0") / 100000,
					Convert.ToDouble(data[i].Get("Godina izgradnje") ?? "0") / 2020,
					Convert.ToDouble(data[i].Get("Ukupan broj soba") ?? "0") / 1000,
					Convert.ToDouble(data[i].Get("Ukupan broj spratova") ?? "0") / 500
				};

				knn.Add(x, Categorizer.ToCategory(Convert.ToDouble(data[i].Price)));
			}

			Console.WriteLine("Test\t\tTrue\t\t\tPredicted");
			for (var i = train; i < data.Count; ++i)
			{
				var x = new double[5]
				{
					Location.ToDistance(data[i].Get("Deo Grada")),
					Convert.ToDouble(data[i].Get("Kvadratura")?.Split(' ')[0] ?? "0") / 100000,
					Convert.ToDouble(data[i].Get("Godina izgradnje") ?? "0") / 2020,
					Convert.ToDouble(data[i].Get("Ukupan broj soba") ?? "0") / 1000,
					Convert.ToDouble(data[i].Get("Ukupan broj spratova") ?? "0") / 500
				};

				var y = Categorizer.ToCategory(Convert.ToDouble(data[i].Price));
				var yh = knn.Predict(x);

				Console.WriteLine($"{i - train + 1}:\t\t{y}\t\t{yh}\t\t{(y == yh ? "T" : string.Empty)}");
			}
		}

		public static async Task Run()
		{
			Console.Write("[L]inear regression / [N]earest Neighbors: ");
			var t = Console.ReadLine().ToLowerInvariant()[0];
			if (t == 'l') LinearRegression();
			if (t == 'n') NearestNeighbors();
		}
	}
}
