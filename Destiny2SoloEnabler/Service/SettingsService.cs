using Destiny2SoloEnabler.Enums;
using Microsoft.Win32;
using System;

namespace Destiny2SoloEnabler.Service;

internal static class SettingsService
{
    private static string _softwareName = "D2SE";

    public static string GetSettingsValue(KeyNames keyName) => GetSettingsValue(keyName.ToString());

    public static string GetSettingsValue(string settingsName)
    {
        string regValue = String.Empty;
        RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");

        if (key is not null)
        {
            regValue = (string)key.GetValue(settingsName);
        }

        return regValue;
    }


    public static bool GetSettingsBooleanValue(KeyNames keyName) => Convert.ToBoolean(GetSettingsValue(keyName));

    public static bool GetSettingsBooleanValue(string settingsName)
    {
        string value = GetSettingsValue(settingsName);
        return Convert.ToBoolean(value);
    }


    public static void SetSettingsValue(KeyNames keyName, string settingsValue) => SetSettingsValue(keyName.ToString(), settingsValue);

    public static void SetSettingsValue(string settingsName, string settingsValue)
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");
        key.SetValue(settingsName, settingsValue);
    }


    public static void DeleteSetting(KeyNames keyName) => DeleteSetting(keyName.ToString());

    public static void DeleteSetting(string settingsName)
    {
        var subKey = Registry.CurrentUser.OpenSubKey($@"SOFTWARE\{_softwareName}", true);
        if (subKey is not null)
        {
            subKey.DeleteValue(settingsName);
        }
    }
}
