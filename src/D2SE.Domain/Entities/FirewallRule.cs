using D2SE.Domain.Constants;

namespace D2SE.Domain.Entities;

public record FirewallRule
{
    public static string RuleName = D2SEConstants.ProgramName;
    public string PortValue { get; set; } = D2SEConstants.PortRange;
    public static string Description => "Used by the program Solo Enabler. Its use is to enable solo play in Destiny 2.";

    public bool IsOut { get; set; } = true;
    public bool IsUDP { get; set; } = true;

    public static FirewallRule CreateRule()
        => new();

    public static FirewallRule CreateRule(string portValue)
        => new FirewallRule() with { PortValue = portValue };
}
