using System;

namespace repplib
{
	public static class Parsing
	{
		public static decimal? ToPrice(this string str)
		{
			try { return Convert.ToDecimal(str.Remove(str.Length - 4).Replace(" ", string.Empty)); }
			catch { return null; }
		}
		public static decimal? ToSpace(this string str)
		{
			try { return Convert.ToDecimal(str.Split(' ')[0]); }
			catch { return null; }
		}
	}
}
