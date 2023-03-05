using Destiny2SoloEnabler.Models;
using NetFwTypeLib;
using System;
using System.Linq;

namespace Destiny2SoloEnabler.Service;

internal class SoloPlayService
{
    private Type _firewallPolicyType;
    private INetFwPolicy2 _firewallPolicy;

    public SoloPlayService()
    {
        _firewallPolicyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
        _firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(_firewallPolicyType);
    }

    public static SoloPlayService Instance()
    {
        var service = new SoloPlayService();
        return service;
    }

    public bool DoesFirewallRuleExist(string ruleName)
    {
        bool ruleExists = _firewallPolicy.Rules.Cast<INetFwRule>().Any(rule => rule.Name == ruleName);
        return ruleExists;
    }

    public void RemoveFirewallRule(string ruleName)
    {
        if(!DoesFirewallRuleExist(ruleName))
        {
            return;
        }

        foreach(INetFwRule rule in _firewallPolicy.Rules)
        {
            if(rule.Name == ruleName)
            {
                _firewallPolicy.Rules.Remove(ruleName);
            }
        }
    }

    public void CreateFirewallRule(FirewallRule rule)
    {
        // Create the firewall rule.
        INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

        // Set the values for the rule.
        inboundRule.Action = rule.Action;
        inboundRule.Description = rule.Description;
        inboundRule.Direction = rule.Direction;
        inboundRule.Enabled = rule.IsEnabled;
        inboundRule.Name = rule.RuleName;
        inboundRule.Protocol = rule.Protocol;
        inboundRule.RemotePorts = rule.PortValue;

        // Add the rule to the firewall policy.
        _firewallPolicy.Rules.Add(inboundRule);
    }

    public void CreateFirewallRules(FirewallRule rule) 
    {
        // Create a total of 4 rules. Out: true and false. Udp: true and false.
        for(int i = 0; i < 2; i++)
        {
            rule.IsOut = i == 0 ? true : false;

            for(int j = 0; j < 2; j++)
            {
                rule.IsUDP = j == 0 ? true : false;
                CreateFirewallRule(rule);
            }
        }
    }
}


// SoloplayService.Instance().DoesFirewallRuleExist("dsadas");
// SoloplayService.Instance().RemoveFirewallRule("dsadwa");
// SoloplayService.Instance().CreateFirewallRule(new FirewallRule() { /* ... */ });