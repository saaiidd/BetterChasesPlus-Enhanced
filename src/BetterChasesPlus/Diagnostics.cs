using System;
using System.IO;

namespace BetterChasesPlus;

internal static class Diagnostics
{
	private static readonly object Sync = new object();

	public static void Info(string message)
	{
		Write("INFO", message);
	}

	public static void Error(string operation, Exception exception)
	{
		Write("ERROR", operation + ": " + exception);
	}

	private static void Write(string level, string message)
	{
		try
		{
			lock (Sync)
			{
				StoragePaths.EnsureDataDirectory();
				File.AppendAllText(
					StoragePaths.LogFile,
					DateTime.UtcNow.ToString("O") + " [" + level + "] " + message + Environment.NewLine);
			}
		}
		catch
		{
			// Diagnostics must not terminate a gameplay script.
		}
	}
}
