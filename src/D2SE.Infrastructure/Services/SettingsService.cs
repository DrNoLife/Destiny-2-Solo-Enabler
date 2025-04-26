using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using Microsoft.Win32;

namespace D2SE.Infrastructure.Services;

#pragma warning disable CA1416 
public class SettingsService : ISettingsService
{
    private const string SoftwareName = "D2SE";
    private const string RegistryLocation = $@"SOFTWARE\{SoftwareName}";

    public bool CheckIfSettingExists(SettingsNames setting)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RegistryLocation, writable: false);

        if (key is null)
        {
            return false;
        }

        return key.GetValue(setting.ToString()) is not null;
    }

    public T GetSettingsValue<T>(SettingsNames setting)
        => GetSettingsValue<T>(setting.ToString());

    public void SetSettingsValue(SettingsNames setting, string settingsValue)
        => SetSettingsValue(setting.ToString(), settingsValue);

    public T GetSettingsValue<T>(string settingsName)
    {
        string value = GetSettingsValueAsString(settingsName);

        if (String.IsNullOrEmpty(value))
        {
            value = typeof(T) switch
            {
                Type t when t == typeof(bool) => "false",
                Type t when t == typeof(string) => String.Empty,
                Type t when t == typeof(int) => "0",
                _ => throw new NotSupportedException($"Type {typeof(T)} is not supported.")
            };
        }

        object result = typeof(T) switch
        {
            Type t when t == typeof(bool) => Convert.ToBoolean(value),
            Type t when t == typeof(string) => value,
            Type t when t == typeof(int) => Convert.ToInt32(value),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported.")
        };

        return (T)result;
    }

    public void SetSettingsValue(string settingsName, string settingsValue)
    {
        RegistryKey key = GetRegistryKey();
        key.SetValue(settingsName, settingsValue);
    }

    private static string GetSettingsValueAsString(string settingsName)
    {
        string regValue = String.Empty;

        RegistryKey key = GetRegistryKey();

        if (key is not null && key.GetValue(settingsName) is not null)
        {
            regValue = (key.GetValue(settingsName) as string) ?? String.Empty;
        }

        return regValue;
    }

    private static RegistryKey GetRegistryKey()
        => Registry.CurrentUser.CreateSubKey(RegistryLocation);
}
#pragma warning restore CA1416