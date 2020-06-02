using System.ComponentModel.DataAnnotations;

namespace RealEstates.Models
{
	public class Property
	{
		[Key] public int ID { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public virtual RealEstate RealEstate { get; set; }
	}
}
