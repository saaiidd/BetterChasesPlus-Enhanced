using System;
using System.IO;

namespace BetterChasesPlus;

internal static class StoragePaths
{
	private const string ProductDirectory = "BetterChasesPlus";
	private const string EditionDirectory = "Enhanced";

	public static string DataDirectory { get; } = Path.Combine(
		Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
		ProductDirectory,
		EditionDirectory);

	public static string ConfigFile { get; } = Path.Combine(DataDirectory, "BetterChasesConfig.xml");

	public static string WarrantsFile { get; } = Path.Combine(DataDirectory, "BetterChasesWarrants.xml");

	public static string LogFile { get; } = Path.Combine(DataDirectory, "BetterChasesPlus.Enhanced.log");

	public static string LegacyConfigFile { get; } = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"scripts",
		"BetterChasesConfig.xml");

	public static string LegacyWarrantsFile { get; } = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"scripts",
		"BetterChasesWarrants.xml");

	public static void EnsureDataDirectory()
	{
		Directory.CreateDirectory(DataDirectory);
	}

	public static void MigrateLegacyFiles()
	{
		EnsureDataDirectory();
		MigrateIfNeeded(LegacyConfigFile, ConfigFile);
		MigrateIfNeeded(LegacyWarrantsFile, WarrantsFile);
	}

	private static void MigrateIfNeeded(string source, string destination)
	{
		if (!File.Exists(destination) && File.Exists(source))
		{
			try
			{
				File.Copy(source, destination, overwrite: false);
				Diagnostics.Info("Migrated legacy data file: " + Path.GetFileName(source));
			}
			catch (IOException exception)
			{
				Diagnostics.Error("Migrating legacy data file " + Path.GetFileName(source), exception);
			}
			catch (UnauthorizedAccessException exception)
			{
				Diagnostics.Error("Migrating legacy data file " + Path.GetFileName(source), exception);
			}
		}
	}
}
