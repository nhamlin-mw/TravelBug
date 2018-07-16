// Decompiled with JetBrains decompiler
// Type: Encore.Utilities.ConfigurationUtility
// Assembly: Nansen.Encore.Core, Version=11.2.5.0, Culture=neutral, PublicKeyToken=null
// MVID: CC483883-96C0-4B49-8B91-FF7427867AC7
// Assembly location: C:\sourcecode\advanced-energy\packages\Nansen.Encore.Core.11.2.5\lib\net461\Nansen.Encore.Core.dll

using System;
using System.Configuration;

namespace TravelBug.Core.Utility.Nansen.Encore
{
	/// <summary>Utilities related to configuration files</summary>
	public static class ConfigurationUtility
	{
		/// <summary>
		/// Returns a boolean appsetting, can be used when the service locator is not yet initialized
		/// </summary>
		internal static bool GetBoolAppsetting(string key, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(ConfigurationManager.AppSettings[key], out result))
				return defaultValue;
			return result;
		}

		/// <summary>
		/// Tries to read an appsetting strongly typed by using <see cref="M:System.Convert.ChangeType(System.Object,System.TypeCode)" /> to convert the string value.
		/// </summary>
		/// <typeparam name="T">Target type</typeparam>
		/// <param name="key">The appsetting key</param>
		/// <param name="fallback">Fallback value to return if the value is not found, or can not be converted to the target type if <paramref name="throwIfInvalid" /> is false.</param>
		/// <param name="throwIfInvalid">If set true, the function will throw if conversion fails. It will *not* throw if the config value is null (not found).</param>
		public static T GetAppSetting<T>(string key, T fallback, bool throwIfInvalid = false)
		{
			string appSetting = ConfigurationManager.AppSettings[key];
			if (appSetting == null)
				return fallback;
			return (T)Convert.ChangeType(appSetting, typeof(T));
		}
	}
}