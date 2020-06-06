using System;

namespace repplib
{
	public static class Logging
	{
		public static void Dump(this Exception ex)
		{
			Console.Error.WriteLine($"{ex.GetType().FullName}: {ex.Message}");
			if (ex.InnerException != null) Console.Error.Write($"Inner exception: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}");
			Console.Error.WriteLine(ex.StackTrace);
			Console.Error.WriteLine();
		}
	}
}
