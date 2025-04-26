using D2SE.Domain.Entities;
using D2SE.Domain.Interfaces.Infrastructure;
using NetFwTypeLib;

namespace D2SE.Infrastructure.Services;

#pragma warning disable CA1416 
public class FirewallService : IFirewallService
{
    private Type _firewallPolicyType;
    private INetFwPolicy2 _firewallPolicy;
    private readonly IAlertService _alertService;

    public FirewallService(IAlertService alertService)
    {
        _alertService = alertService;

        _firewallPolicyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2")
            ?? throw new Exception("Failed to access firewall.");

        _firewallPolicy = Activator.CreateInstance(_firewallPolicyType) as INetFwPolicy2 
            ?? throw new Exception("Failed to create instance of firewall policy.");
    }

    public void CreateFirewallRule(FirewallRule ruleEntity)
    {
        // Create the firewall rule.
        var type = Type.GetTypeFromProgID("HNetCfg.FWRule")
            ?? throw new Exception("Failed to create a firewall rule type based on program Id.");

        var inboundRule = Activator.CreateInstance(type) as INetFwRule2
            ?? throw new Exception("Failed to create a firewall rule object.");

        // Set the values for the rule.
        inboundRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
        inboundRule.Description = FirewallRule.Description;
        inboundRule.Direction = ruleEntity.IsOut
            ? NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT
            : NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
        inboundRule.Enabled = true;
        inboundRule.Name = FirewallRule.RuleName;
        inboundRule.Protocol = (int)(ruleEntity.IsUDP
            ? NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP
            : NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
        inboundRule.RemotePorts = ruleEntity.PortValue;

        // Add the rule to the firewall policy.
        _firewallPolicy.Rules.Add(inboundRule);
    }

    public void CreateFirewallRules(FirewallRule ruleEntity)
    {
        try
        {
            // Create a total of 4 rules.
            // Out: true and false.
            // Udp: true and false.
            for (int i = 0; i < 2; i++)
            {
                ruleEntity = ruleEntity with { IsOut = i == 0 };

                for (int j = 0; j < 2; j++)
                {
                    ruleEntity = ruleEntity with { IsUDP = j == 0 };
                    CreateFirewallRule(ruleEntity);
                }
            }
        }
        catch (Exception ex)
        {
            _alertService.ShowAlert("Error creating firewall rule", ex.Message);
        }
    }

    public bool FirewallRulesExists()
    {
        bool ruleExists = _firewallPolicy
            .Rules
            .Cast<INetFwRule>()
            .Any(rule => rule.Name.Equals(FirewallRule.RuleName, StringComparison.OrdinalIgnoreCase));

        return ruleExists;
    }

    public void RemoveFirewallRules()
    {
        if (!FirewallRulesExists())
        {
            return;
        }

        foreach (INetFwRule rule in _firewallPolicy.Rules)
        {
            if (rule.Name.Equals(FirewallRule.RuleName, StringComparison.OrdinalIgnoreCase))
            {
                _firewallPolicy.Rules.Remove(FirewallRule.RuleName);
            }
        }
    }
}
#pragma warning restore CA1416