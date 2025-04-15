using NetFwTypeLib;

namespace Destiny2SoloEnabler.Models;

internal class FirewallRule
{
    public string RuleName { get; set; } = "Firewall testing via C#";
    public string PortValue { get; set; } = "8000-8005";
    public bool IsOut { get; set; } = true;
    public bool IsUDP { get; set; } = true;
    public string Description { get; set; } = "Used by the program Solo Enabler. Its use is to enable solo play in Destiny 2.";
    public bool IsEnabled { get; set; } = true;

    public NET_FW_ACTION_ Action => NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
    public NET_FW_RULE_DIRECTION_ Direction => IsOut ? NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT : NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
    public int Protocol => (int)(IsUDP ? NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP : NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
}
