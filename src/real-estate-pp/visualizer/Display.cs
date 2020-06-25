using System;
using System.Collections.Generic;
using System.Windows.Forms;

using repplib;

namespace visualizer
{
	public partial class Display : Form
	{
		private (string, string, IEnumerable<(string, decimal)>)[] Data = new (string, string, IEnumerable<(string, decimal)>)[9];
		private int Current = 0;

		public Display()
		{
			InitializeComponent();
		}

		private void Display_Load(object sender, EventArgs e)
		{
			// Load TopCityParts
			var topCityParts = new List<(string, decimal)>();
			foreach (var part in Queries.TopCityParts("Beograd", 8))
				topCityParts.Add((part.Item1, part.Item2));
			Data[0] = ("8 Parts of Belgrade with most Real Estates", "City parts", topCityParts);

			// Load SalesInCountryBySpace
			var salesBySpace = new List<(string, decimal)>()
			{
				("<36", 0),
				("36-50", 0),
				("51-65", 0),
				("66-80", 0),
				("81-95", 0),
				("96-110", 0),
				(">110", 0)
			};
			foreach (var s in Queries.SalesInCountryBySpace("Srbija", Category.Appartment))
			{
				var space = (int)(s ?? 0);
				var index = Math2.Clamp(space - 21, 0, 90) / 15;
				salesBySpace[index] = (salesBySpace[index].Item1, salesBySpace[index].Item2 + 1);
			}
			Data[1] = ("Appartments for sale by space, in Belgrade", "Space", salesBySpace);

			// Load YearsOfConstructions
			var years = new List<(string, decimal)>()
			{
				("1950-1959", 0),
				("1960-1969", 0),
				("1970-1979", 0),
				("1980-1989", 0),
				("1990-1999", 0),
				("2000-2009", 0),
				("2010-2019", 0)
			};
			foreach (var year in Queries.YearsOfConstructions())
			{
				var index = (year - 1950) / 10;
				if (0 <= index && index < years.Count)
					years[index] = (years[index].Item1, years[index].Item2 + 1);
			}
			Data[2] = ("Count of built Real Estates by decade", "Decade", years);

			// Load CountOfSalesAndRentsForTopCities
			var top = 1;
			foreach (var c in Queries.CountOfSalesAndRentsForTopCities(5))
			{
				var city = new List<(string, decimal)>()
				{
					("Sales", c.Item2),
					("Rents", c.Item3),
				};
				Data[2 + top] = ($"Sales/Rents ratio in Top{top} City ({c.Item1})", "Type", city);
				++top;
			}

			// Load SalesByPrice
			var prices = new List<(string, decimal)>()
			{
				("< 50.000 €", 0),
				("50.000-99.999 €", 0),
				("100.000-149.999 €", 0),
				("150.000-199.999 €", 0),
				("200.000+ €", 0),
			};
			foreach (var price in Queries.SalesByPrice())
			{
				var index = Math.Min((int)(price ?? 0), 200000) / 50000;
				prices[index] = (prices[index].Item1, prices[index].Item2 + 1);
			}
			Data[8] = ("Sales by price", "Price", prices);

			LoadDataToChart();
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (--Current == -1) Current = Data.Length - 1;
			LoadDataToChart();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (++Current == Data.Length) Current = 0;
			LoadDataToChart();
		}

		private void LoadDataToChart()
		{
			chart.Titles[0].Text = Data[Current].Item1;
			chart.Series.Clear();
			chart.Series.Add(Data[Current].Item2);
			foreach (var point in Data[Current].Item3)
				chart.Series[Data[Current].Item2].Points.AddXY(point.Item1, point.Item2);
		}
	}
}
