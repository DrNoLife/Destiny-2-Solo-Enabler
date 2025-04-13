namespace D2SE.Domain.Entities;

public record AppSettings(
    bool AlwaysOnTop,
    bool EnableHotkey,
    bool PersistentRules,
    bool InvertFunctionality)
{
    public static AppSettings CreateDefaultSettings()
        => new(false, false, false, false);
};
