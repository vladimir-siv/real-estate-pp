using System;
using System.Collections.Generic;
using RealEstates.Models;

namespace repplib
{
	public static class Collections
	{
		public static T TakeThat<T>(this IEnumerable<T> collection, Func<T, T, bool> comparator) where T : class
		{
			var result = (T)null;

			foreach (var e in collection)
			{
				if (result == null || comparator(e, result))
				{
					result = e;
				}
			}

			return result;
		}

		public static bool Has(this RealEstate @this, string name, string value)
		{
			return @this.Properties.Find(p => p.Name == name)?.Value == value;
		}

		public static string Get(this RealEstate @this, string name)
		{
			return @this.Properties.Find(p => p.Name == name)?.Value;
		}
	}
}
