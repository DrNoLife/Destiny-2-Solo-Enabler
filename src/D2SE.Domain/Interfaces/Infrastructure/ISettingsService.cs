namespace D2SE.Domain.Interfaces.Infrastructure;

public interface ISettingsService
{
    public bool GetSettingsValue(string settingsName);
    public void SetSettingsValue(string settingsName, string settingsValue);
}
