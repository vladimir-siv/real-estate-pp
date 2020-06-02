using System;
using System.Linq;

using OpenQA.Selenium.Chrome;

using RealEstates.Models;
using dbdriver;

namespace scraper
{
	public static class Tests
	{
		private static void TestChromeDriver()
		{
			var options = new ChromeOptions();
			options.AddArguments("--disable-gpu");

			var driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://reddit.com");

			/*
			driver.FindElementByXPath("somepath").Click();
			driver.FindElementByXPath("somepath").SendKeys("Test");
			driver.FindElementByXPath("somepath").SendKeys(Keys.Enter);
			*/
		}

		private static void TestDatabase(bool insertData = false)
		{
			try
			{
				Console.Write("Creating context . . .");

				using (var db = new RealEstateModel())
				{
					Console.WriteLine(" Done!");

					if (insertData)
					{
						Console.WriteLine();
						Console.Write("Inserting data . . .");
						var prop = new Property { Name = "Broj soba", Value = "5" };
						db.Properties.Add(prop);
						db.SaveChanges();
						Console.WriteLine(" Done!");
					}

					Console.WriteLine();
					Console.WriteLine("Printing data:");
					Console.WriteLine();
					var query = from p in db.Properties select p;
					foreach (var item in query) Console.WriteLine($"{item.Name}: {item.Value}");

					Console.WriteLine();
					Console.WriteLine("Done!");
				}
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
		}

		private static void TestDeleteDatabase()
		{
			using (var db = new RealEstateModel())
			{
				db.Database.Delete();
			}
		}

		public static void Run()
		{
			TestDatabase();
		}
	}
}
