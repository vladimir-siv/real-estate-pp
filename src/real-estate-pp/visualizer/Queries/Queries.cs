using System;
using System.Collections.Generic;
using System.Linq;

using dbdriver;
using repplib;

using AppContext = repplib.AppContext;

namespace visualizer
{
	public static class Queries
	{
		private static readonly RealEstateModel DB = AppContext.Resolve<RealEstateModel>();

		public static IEnumerable<(string, int)> TopCityParts(string city = "Beograd", int count = 8)
		{
			var parts =
				from p1 in DB.Properties
				join p2 in DB.Properties
				on p1.RealEstate.ID equals p2.RealEstate.ID
				where p1.Name == "Grad" && p1.Value == city && p2.Name == "Deo Grada"
				group p2 by p2.Value into g
				orderby g.Count() descending
				select new { Part = g.Key, Count = g.Count() };

			foreach (var part in parts.Take(count).ToList()) yield return (part.Part, part.Count);
		}

		public static IEnumerable<decimal?> SalesInCountryBySpace(string country = "Srbija", Category category = Category.Appartment)
		{
			var vals = category.ToValues();

			var parts =
				from p1 in DB.Properties
				join p2 in DB.Properties on p1.RealEstate.ID equals p2.RealEstate.ID
				join p3 in DB.Properties on p1.RealEstate.ID equals p3.RealEstate.ID
				join p4 in DB.Properties on p1.RealEstate.ID equals p4.RealEstate.ID
				where
					p1.Name == "Zemlja" && p1.Value == country
					&&
					p2.Name == "Kategorija" && vals.Contains(p2.Value)
					&&
					p3.Name == "Transakcija" && p3.Value == "Prodaja"
					&&
					p4.Name == "Kvadratura"
				select p4.Value;

			foreach (var s in parts.ToList()) yield return s.ToSpace();
		}

		public static IEnumerable<int> YearsOfConstructions()
		{
			var years =
				from pr in DB.Properties
				where pr.Name == "Godina izgradnje"
				select pr.Value;

			foreach (var y in years) yield return Convert.ToInt32(y);
		}

		public static IEnumerable<(string, int, int)> CountOfSalesAndRentsForTopCities(int countOfCities = 5)
		{
			var cities =
				from pr in DB.Properties
				where pr.Name == "Grad"
				group pr by pr.Value into g
				orderby g.Count() descending
				select g.Key;

			foreach (var city in cities.Take(countOfCities).ToList())
			{
				var snr =
					from p1 in DB.Properties
					join p2 in DB.Properties on p1.RealEstate.ID equals p2.RealEstate.ID
					where
						p1.Name == "Grad" && p1.Value == city
						&&
						p2.Name == "Transakcija"
					group p2 by p2.Value into g
					select new { Type = g.Key, Count = g.Count() };

				var counts = snr.ToList();

				var sales = counts[0].Type == "Prodaja" ? counts[0].Count : counts[1].Count;
				var rents = counts[0].Type == "Prodaja" ? counts[1].Count : counts[0].Count;

				yield return (city, sales, rents);
			}
		}

		public static IEnumerable<decimal?> SalesByPrice()
		{
			var prices =
				from pr in DB.Properties
				join re in DB.RealEstates on pr.RealEstate.ID equals re.ID
				where
					pr.Name == "Transakcija" && pr.Value == "Prodaja"
				select re.Price;

			foreach (var price in prices.ToList()) yield return price;
		}
	}
}
