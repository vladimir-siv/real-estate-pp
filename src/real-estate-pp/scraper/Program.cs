﻿using System;

namespace scraper
{
	public static class Program
	{
		private static void Main(string[] args)
		{
			App.Init();
			try { Controller.Run().Wait(); }
			catch (Exception ex) { ex.Dump(); }
			App.Dispose();
			Console.WriteLine($"{Environment.NewLine}Press any key to exit . . .");
			Console.ReadKey(true);
		}
	}
}
