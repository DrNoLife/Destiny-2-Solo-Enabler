using D2SE.Domain.Enums;

namespace D2SE.Domain.Interfaces.Infrastructure;

public interface ISettingsService
{
    public bool CheckIfSettingExists(SettingsNames setting);
    public T GetSettingsValue<T>(SettingsNames setting);
    public T GetSettingsValue<T>(string settingsName);
    public void SetSettingsValue(string settingsName, string settingsValue);
    public void SetSettingsValue(SettingsNames setting, string settingsValue);
}
