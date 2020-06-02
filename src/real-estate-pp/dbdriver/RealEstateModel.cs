using System.Data.Entity;
using RealEstates.Models;

namespace dbdriver
{
	public class RealEstateModel : DbContext
	{
		public RealEstateModel() : base("name=RealEstateModel") { }

		public virtual DbSet<RealEstate> RealEstates { get; set; }
		public virtual DbSet<Property> Properties { get; set; }
	}
}
