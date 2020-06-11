using System;
using System.Text;

namespace repplib
{
	public static class Logging
	{
		public static void Dump(this Exception ex)
		{
			Console.Error.WriteLine($"{ex.GetType().FullName}: {ex.Message}");
			Console.Error.WriteLine(ex.StackTrace);

			Console.Error.WriteLine();

			if (ex.InnerException != null)
			{
				Console.Error.WriteLine($"Inner exception: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}");
				Console.Error.WriteLine(ex.InnerException.StackTrace);
			}

			Console.Error.WriteLine();
		}
		public static string DumpAsString(this Exception ex)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"{ex.GetType().FullName}: {ex.Message}");
			sb.AppendLine(ex.StackTrace);

			sb.AppendLine();

			if (ex.InnerException != null)
			{
				sb.AppendLine($"Inner exception: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}");
				sb.AppendLine(ex.InnerException.StackTrace);
			}

			return sb.ToString();
		}
	}
}
