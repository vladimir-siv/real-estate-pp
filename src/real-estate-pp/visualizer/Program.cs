using System;
using System.Windows.Forms;
using repplib;

namespace visualizer
{
	public static class Program
	{
		[STAThread] private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			App.Init();
			try { Application.Run(new Display()); }
			catch (Exception ex) { MessageBox.Show(ex.DumpAsString(), "An error has occured", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			finally { App.Dispose(); }
		}
	}
}
