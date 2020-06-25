using System;

namespace repplib
{
	public static class Math2
	{
		private static Random RNG = new Random();

		public static T Clamp<T>(T value, T min, T max)
		{
			dynamic val = value;
			if (val < min) return min;
			if (val > max) return max;
			return value;
		}

		public static double Random()
		{
			return RNG.NextDouble();
		}

		public static double Random(double min, double max)
		{
			return min + (max - min) * Random();
		}
	}
}
