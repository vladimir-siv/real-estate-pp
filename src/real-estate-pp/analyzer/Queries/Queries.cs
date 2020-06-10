using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RealEstates.Models;

using dbdriver;
using repplib;

using AppContext = repplib.AppContext;

namespace analyzer
{
	public static class Queries
	{
		private static readonly RealEstateModel DB = AppContext.Resolve<RealEstateModel>();

		public static int CountOfSales()
		{
			var sales =
				from p in DB.Properties
				where p.Name == "Transakcija" && p.Value == "Prodaja"
				select p.ID;

			return sales.Count();
		}

		public static int CountOfRents()
		{
			var sales =
				from p in DB.Properties
				where p.Name == "Transakcija" && p.Value == "Izdavanje"
				select p.ID;

			return sales.Count();
		}

		public static IEnumerable<(string, int)> CountOfSalesPerCity()
		{
			var sales =
				from p1 in DB.Properties
				join p2 in DB.Properties
				on p1.RealEstate.ID equals p2.RealEstate.ID
				where p1.Name == "Grad" && p2.Name == "Transakcija" && p2.Value == "Prodaja"
				group p1 by p1.Value into g
				select new { City = g.Key, Count = g.Count() };

			foreach (var city in sales.ToList()) yield return (city.City, city.Count);
		}

		public static IEnumerable<(string, int)> CountOfRegistrations(Category category)
		{
			var vals = category.ToValues();

			var registrations =
				from p1 in DB.Properties
				join p2 in DB.Properties
				on p1.RealEstate.ID equals p2.RealEstate.ID
				where p1.Name == "Kategorija" && vals.Contains(p1.Value) && p2.Name == "Uknjiženo"
				group p2 by p2.Value into g
				select new { Registered = g.Key, Count = g.Count() };

			foreach (var registration in registrations.ToList()) yield return (registration.Registered, registration.Count);
		}

		public static IEnumerable<(string, decimal?)> TopSaleList(Category category, int count = 20, string country = "Srbija")
		{
			var vals = category.ToValues();

			var top =
				from p1 in DB.Properties
				join p2 in DB.Properties on p1.RealEstate.ID equals p2.RealEstate.ID
				join p3 in DB.Properties on p1.RealEstate.ID equals p3.RealEstate.ID
				join re in DB.RealEstates on p1.RealEstate.ID equals re.ID
				where
					p1.Name == "Transakcija" && p1.Value == "Prodaja"
					&&
					p2.Name == "Kategorija" && vals.Contains(p2.Value)
					&&
					p3.Name == "Zemlja" && p3.Value == country
				orderby re.Price descending
				select new { re.Description, re.Price };

			foreach (var re in top.Take(count).ToList()) yield return (re.Description, re.Price);
		}

		public static IEnumerable<(string, decimal?)> TopRentList(Category category, int count = 100)
		{
			var vals = category.ToValues();

			var top =
				from p1 in DB.Properties
				join p2 in DB.Properties on p1.RealEstate.ID equals p2.RealEstate.ID
				join re in DB.RealEstates on p1.RealEstate.ID equals re.ID
				where
					p1.Name == "Transakcija" && p1.Value == "Izdavanje"
					&&
					p2.Name == "Kategorija" && vals.Contains(p2.Value)
				orderby re.Price descending
				select new { re.Description, re.Price };

			foreach (var re in top.Take(count).ToList()) yield return (re.Description, re.Price);
		}

		public static IEnumerable<(string, decimal?)> TopListByYear(int year = 2019)
		{
			var top =
				from pp in DB.Properties
				join re in DB.RealEstates on pp.RealEstate.ID equals re.ID
				where pp.Name == "Godina izgradnje" && pp.Value == year.ToString()
				orderby re.Price descending
				select new { re.Description, re.Price };

			foreach (var re in top.ToList()) yield return (re.Description, re.Price);
		}

		public static (string, decimal) BestRealEstateByProperty(string property, Func<string, decimal> val = null)
		{
			if (val == null) val = Convert.ToDecimal;

			var top =
				from pp in DB.Properties
				join re in DB.RealEstates on pp.RealEstate.ID equals re.ID
				where pp.Name == property
				select new { re.Description, pp.Value };

			var e = top.ToList().TakeThat((c, b) => val(c.Value ?? "0") > val(b.Value ?? "0"));
			return (e.Description, val(e.Value ?? "0"));
		}
	}
}
