using System;
using System.IO;
using System.Xml.Serialization;

namespace BetterChasesPlus;

internal static class XmlPersistence
{
	public static void Save<T>(string path, T value, string rootName)
	{
		string directory = Path.GetDirectoryName(path);
		if (!string.IsNullOrEmpty(directory))
		{
			Directory.CreateDirectory(directory);
		}

		string temporaryPath = path + ".tmp";
		XmlSerializer serializer = CreateSerializer(typeof(T), rootName);
		using (FileStream stream = new FileStream(temporaryPath, FileMode.Create, FileAccess.Write, FileShare.None))
		{
			serializer.Serialize(stream, value);
			stream.Flush();
		}

		if (File.Exists(path))
		{
			string backupPath = path + ".bak";
			if (File.Exists(backupPath))
			{
				File.Delete(backupPath);
			}
			File.Replace(temporaryPath, path, backupPath);
		}
		else
		{
			File.Move(temporaryPath, path);
		}
	}

	public static bool TryLoad<T>(string path, string rootName, out T value)
	{
		value = default(T);
		if (!File.Exists(path))
		{
			return false;
		}

		XmlSerializer serializer = CreateSerializer(typeof(T), rootName);
		using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			value = (T)serializer.Deserialize(stream);
		}
		return true;
	}

	private static XmlSerializer CreateSerializer(Type type, string rootName)
	{
		XmlAttributes attributes = new XmlAttributes
		{
			XmlRoot = new XmlRootAttribute(rootName)
		};
		XmlAttributeOverrides overrides = new XmlAttributeOverrides();
		overrides.Add(type, attributes);
		return new XmlSerializer(type, overrides);
	}
}
