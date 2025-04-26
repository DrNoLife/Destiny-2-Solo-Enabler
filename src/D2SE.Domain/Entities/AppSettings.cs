using D2SE.Domain.Constants;

namespace D2SE.Domain.Entities;

public record AppSettings(
    bool AlwaysOnTop,
    bool EnableHotkey,
    bool PersistentRules,
    bool InvertFunctionality,
    bool EnableNotifications,
    bool OverridePortsToBlock,
    string CustomPortRangeToBlock)
{
    public static AppSettings CreateDefaultSettings()
        => new(false, false, false, false, true, false, D2SEConstants.PortRange);
};
