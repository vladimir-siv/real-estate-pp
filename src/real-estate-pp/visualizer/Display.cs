using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using dbdriver;

using AppContext = repplib.AppContext;

namespace visualizer
{
	public partial class Display : Form
	{
		private static readonly RealEstateModel DB = AppContext.Resolve<RealEstateModel>();

		public static int CountOfSales()
		{
			var sales =
				from p in DB.Properties
				where p.Name == "Transakcija" && p.Value == "Prodaja"
				select p.ID;

			return sales.Count();
		}

		public Display()
		{
			InitializeComponent();

			Load += Display_Load;
		}

		private void Display_Load(object sender, EventArgs e)
		{
			MessageBox.Show(CountOfSales().ToString());
		}
	}
}
