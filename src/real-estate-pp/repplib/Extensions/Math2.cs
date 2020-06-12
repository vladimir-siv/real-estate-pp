namespace repplib
{
	public static class Math2
	{
		public static T Clamp<T>(T value, T min, T max)
		{
			dynamic val = value;
			if (val < min) return min;
			if (val > max) return max;
			return value;
		}
	}
}
