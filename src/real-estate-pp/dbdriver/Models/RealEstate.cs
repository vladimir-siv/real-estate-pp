using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstates.Models
{
	public class RealEstate
	{
		[Key] public int ID { get; set; }
		public string Description { get; set; }
		public decimal? Price { get; set; }
		public virtual List<Property> Properties { get; set; }
	}
}
