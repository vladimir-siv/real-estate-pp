using System;
using System.Collections.Generic;

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
	}
}
