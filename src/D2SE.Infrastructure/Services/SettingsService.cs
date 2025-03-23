using D2SE.Domain.Interfaces.Infrastructure;
using Microsoft.Win32;

namespace D2SE.Infrastructure.Services;

#pragma warning disable CA1416 
public class SettingsService : ISettingsService
{
    private const string SoftwareName = "D2SE";
    private const string RegistryLocation = $@"SOFTWARE\{SoftwareName}";

    public bool GetSettingsValue(string settingsName)
    {
        string value = GetSettingsValueAsString(settingsName);

        if (String.IsNullOrEmpty(value))
        {
            value = "false";
        }

        return Convert.ToBoolean(value);
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