using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using RealEstates.Models;

using AppContext = repplib.AppContext;

namespace scraper
{
	public class Crawler
	{
		private const string OfferXPath = "/html/body/div[contains(concat(\" \",normalize-space(@class),\" \"),\" list-page \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" container \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-12 \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-12 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" col-lg-6 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" col-xl-7 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" offer-container \")][contains(concat(\" \",normalize-space(@class),\" \"),\" w70-1024 \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" row \")][contains(concat(\" \",normalize-space(@class),\" \"),\" offer \")]/div[contains(concat(\" \",normalize-space(@class),\" \"),\" col-8 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" col-md-9 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" offer-body \")][contains(concat(\" \",normalize-space(@class),\" \"),\" py-2 \")][contains(concat(\" \",normalize-space(@class),\" \"),\" px-3 \")]/h2[contains(concat(\" \",normalize-space(@class),\" \"),\" offer-title \")][contains(concat(\" \",normalize-space(@class),\" \"),\" text-truncate \")][contains(concat(\" \",normalize-space(@class),\" \"),\" w-100 \")]/a";
		
		public int From { get; private set; }
		public int To { get; private set; }

		public Scraper Scraper { get; private set; } = new Scraper();

		public event Action<int, IReadOnlyList<RealEstate>> PageCompleted;
		public event Action<int, Exception> PageError;

		public Crawler(int from, int to)
		{
			if (from <= 0 || to <= 0 || from > to) throw new ArgumentException("'From' and 'To' cannot be 0 and 'From' must be less than or equal to 'To'.");
			From = from;
			To = to;
		}

		public async Task Run()
		{
			await Task.Run(() =>
			{
				var driver = AppContext.Resolve<ChromeDriver>();
				var links = new List<string>(20);
				var estates = new List<RealEstate>(20);

				for (var i = From; i <= To; ++i)
				{
					try
					{
						estates.Clear();

						driver.Navigate().GoToUrl($"https://www.nekretnine.rs/stambeni-objekti/lista/po-stranici/10/stranica/{i}/");

						links.Clear();
						foreach (var offer in driver.FindElementsByXPath(OfferXPath))
							links.Add(offer.GetAttribute("href"));

						foreach (var link in links)
						{
							var estate = Scraper.Scrape(link);
							estates.Add(estate);
						}

						PageCompleted?.Invoke(i, estates);
					}
					catch (Exception ex) { PageError?.Invoke(i, ex); }
				}
			});
		}
	}
}
