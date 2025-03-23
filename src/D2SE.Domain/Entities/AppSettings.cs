namespace D2SE.Domain.Entities;

public record AppSettings(
    bool AlwaysOnTop,
    bool EnableHotkey,
    bool PersistentRules)
{
    public static AppSettings CreateDefaultSettings()
        => new(false, false, false);
};
