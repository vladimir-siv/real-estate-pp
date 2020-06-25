using System;
using System.Threading.Tasks;

using repplib;

namespace predictor
{
	public static class Controller
	{
		public static async Task Run()
		{
			Console.WriteLine("Works");

			Console.WriteLine(Queries.CountOfSales());
		}
	}
}
