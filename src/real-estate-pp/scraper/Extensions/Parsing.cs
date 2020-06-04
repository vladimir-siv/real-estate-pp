using System;

namespace scraper
{
	public static class Parsing
	{
		public static decimal? ToPrice(this string str)
		{
			try { return Convert.ToDecimal(str.Remove(str.Length - 4).Replace(" ", string.Empty)); }
			catch { return null; }
		}
	}
}
