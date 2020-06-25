namespace predictor
{
	public enum Category
	{
		None = 0,
		Below50000 = 1,
		B50kAnd100k = 2,
		B100kAnd150k = 3,
		B150kAnd200k = 4,
		Above200k = 5
	}

	public static class Categorizer
	{
		public static Category ToCategory(double price)
		{
			if (price < 0.0) return Category.None;
			if (price < 50000.0) return Category.Below50000;
			if (price < 100000.0) return Category.B50kAnd100k;
			if (price < 150000.0) return Category.B100kAnd150k;
			if (price < 200000.0) return Category.B150kAnd200k;
			return Category.Above200k;
		}
	}
}
