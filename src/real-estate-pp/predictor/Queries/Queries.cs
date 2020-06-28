using System;
using System.Collections.Generic;
using System.Linq;

using RealEstates.Models;

using dbdriver;
using repplib;

using AppContext = repplib.AppContext;

namespace predictor
{
	public static class Queries
	{
		private static readonly RealEstateModel DB = AppContext.Resolve<RealEstateModel>();

		public static List<RealEstate> GetData()
		{
			var results = new List<RealEstate>();

			var query =
				from re in DB.RealEstates
				select re;

			foreach (var estate in query.ToList())
			{
				if
				(
					estate.Has("Grad", "Beograd")
					&&
					estate.Get("Kategorija").ToCategory() == repplib.Category.Appartment
					&&
					estate.Has("Transakcija", "Prodaja")
				)
				{
					results.Add(estate);
				}
			}

			return results;
		}
	}
}
