using Microsoft.Win32;
using System;

namespace Destiny2SoloEnabler.Service;

internal static class SettingsService
{
    private static readonly string _softwareName = "D2SE";

    public static string GetSettingsValue(string settingsName)
    {
        string regValue = String.Empty;
        RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");

        if (key is not null && key.GetValue(settingsName) is not null)
        {
            regValue = (string)key.GetValue(settingsName)!;
        }

        return regValue;
    }

    public static bool GetSettingsBooleanValue(string settingsName)
    {
        string value = GetSettingsValue(settingsName);

        if (String.IsNullOrEmpty(value))
        {
            value = "false";
        }

        return Convert.ToBoolean(value);
    }

    public static void SetSettingsValue(string settingsName, string settingsValue)
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");
        key.SetValue(settingsName, settingsValue);
    }
}
