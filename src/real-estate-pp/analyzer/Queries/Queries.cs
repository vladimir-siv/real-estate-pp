using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RealEstates.Models;

using dbdriver;
using repplib;

using AppContext = repplib.AppContext;

namespace analyzer
{
	public static class Queries
	{
		private static readonly RealEstateModel DB = AppContext.Resolve<RealEstateModel>();


	}
}
