using System.Collections.Generic;

namespace repplib
{
	public static class CategoryClassifier
	{
		private static readonly string[] houses = new[]
		{
			"Duplex kuće",
			"Porodična kuća",
			"Kuće sa više stanova"
		};
		private static readonly string[] appartments = new[]
		{
			"Garsonjera",
			"Jednosoban stan",
			"Dvosoban stan",
			"Trosoban stan",
			"Četvorosoban stan",
			"Petosoban+ stan",
			"Stanovi"
		};
		private static readonly string[] others = new[]
		{
			"Sobe",
			"Ostali tipovi prostora",
			"Garaže i parking mesta"
		};

		private static HashSet<string> houseCategory = new HashSet<string>(houses);
		private static HashSet<string> appartmentCategory = new HashSet<string>(appartments);
		private static HashSet<string> otherCategory = new HashSet<string>(others);

		public static Category ToCategory(this string category)
		{
			if (houseCategory.Contains(category)) return Category.House;
			if (appartmentCategory.Contains(category)) return Category.Appartment;
			if (otherCategory.Contains(category)) return Category.Other;
			return Category.Unknown;
		}

		public static string[] ToValues(this Category category)
		{
			if (category == Category.House) return houses;
			if (category == Category.Appartment) return appartments;
			if (category == Category.Other) return others;
			return null;
		}
	}
}
