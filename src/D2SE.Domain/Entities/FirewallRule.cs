namespace D2SE.Domain.Entities;

public record FirewallRule
{
    public static string RuleName => "Destiny 2 - Solo-Enabler";
    public string PortValue { get; set; } = "27000-27202,3097";
    public static string Description => "Used by the program Solo Enabler. Its use is to enable solo play in Destiny 2.";

    public bool IsOut { get; set; } = true;
    public bool IsUDP { get; set; } = true;

    public static FirewallRule CreateRule()
        => new();

    public static FirewallRule CreateRule(string portValue)
        => new FirewallRule() with { PortValue = portValue };
}
