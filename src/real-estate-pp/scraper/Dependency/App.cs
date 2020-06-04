using OpenQA.Selenium.Chrome;
using dbdriver;

namespace scraper
{
	public static class App
	{
		static App()
		{
			var options = new ChromeOptions();
			options.AddArguments("--disable-gpu");
			options.AddArguments("--headless");
			options.AddArguments("--silent");
			options.AddArguments("--log-level=3");

			var service = ChromeDriverService.CreateDefaultService();
			service.HideCommandPromptWindow = true;

			var driver = new ChromeDriver(service, options);
			AppContext.Inject(driver);

			var db = new RealEstateModel();
			AppContext.Inject(db);
		}

		public static void Init()
		{

		}

		public static void Dispose()
		{
			var driver = AppContext.Resolve<ChromeDriver>();
			driver.Dispose();

			var db = AppContext.Resolve<RealEstateModel>();
			db.Dispose();
		}
	}
}
