using Microsoft.Win32;
using System;

namespace D2SoloEnabler.Helpers
{
    public static class SettingsStore
    {
        private static string _softwareName = "D2SE";

        public static string GetSettingValue(string settingName)
        {
            string regValue = String.Empty;

            RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");

            if(key != null)
            {
                regValue = (string)key.GetValue(settingName);
            }

            return regValue;
        }

        public static void SetSettingValue(string settingName, string settingValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{_softwareName}");
            key.SetValue(settingName, settingValue);
        }
    }
}
