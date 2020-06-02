using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace scraper
{
	public static class Program
	{
		private static void Test()
		{
			var options = new ChromeOptions();
			options.AddArguments("--disable-gpu");

			var driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://reddit.com");

			driver.FindElementByXPath("somepath").Click();
			driver.Keyboard.SendKeys("Test");
			driver.Keyboard.SendKeys(Keys.Enter);
		}

		private static void Main(string[] args)
		{
			Console.WriteLine("Press any key to exit . . .");
			Console.ReadKey(true);
		}
	}
}
