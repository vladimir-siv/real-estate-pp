using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using RealEstates.Models;

namespace scraper
{
	public class Scraper
	{
		private const string ContainerXPath = "/html/body/div[contains(concat(\" \",normalize-space(@class),\" \"),\" container \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")][contains(concat(\" \",normalize-space(@class),\" \"),\" pt-4 \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-lg-8 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" mb-3 \")]";
		private const string DescriptionXPath = ContainerXPath + "/h1[contains(concat(\" \",normalize-space(@class),\" \"),\" detail-title \")][contains(concat(\" \",normalize-space(@class),\" \"),\" pt-4 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" pb-4 \")]";
		private const string PriceXPath = ContainerXPath + "/h3[contains(concat(\" \",normalize-space(@class),\" \"),\" detail-seo-subtitle-line2 \")]";
		private const string PropRegion1XPath = ContainerXPath + "/div[contains(concat(\" \",normalize-space(@class),\" \"),\" base-inf \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-sm-6 \")]/dl[contains(concat(\" \",normalize-space(@class),\" \"),\" dl-horozontal \")]";
		private const string PropRegion2XPath = ContainerXPath + "/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")][contains(concat(\" \",normalize-space(@class),\" \"),\" pb-3 \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-sm-6 \")]/dl[contains(concat(\" \",normalize-space(@class),\" \"),\" dl-horozontal \")]";

		public RealEstate Scrape(string link)
		{
			var driver = AppContext.Resolve<ChromeDriver>();
			driver.Navigate().GoToUrl(link);

			var estate = new RealEstate
			{
				Description = driver.FindElementByXPath(DescriptionXPath)?.Text?.Trim(),
				Price = driver.FindElementByXPath(PriceXPath).Text.Split(',')[0].ToPrice(),
				Properties = new List<Property>()
			};

			void Parse(IWebElement dl)
			{
				estate.Properties.Add
				(
					new Property
					{
						Name = dl.FindElement(By.TagName("dt")).Text.Trim().Replace(":", string.Empty),
						Value = dl.FindElement(By.TagName("dd")).Text.Trim(),
						RealEstate = estate
					}
				);
			}

			foreach (var dl in driver.FindElementsByXPath(PropRegion1XPath)) Parse(dl);
			foreach (var dl in driver.FindElementsByXPath(PropRegion2XPath)) Parse(dl);

			return estate;
		}
	}
}
