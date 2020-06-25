using System;
using System.Collections.Generic;
using System.Linq;

using dbdriver;
using repplib;

using AppContext = repplib.AppContext;

namespace predictor
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
	}
}
