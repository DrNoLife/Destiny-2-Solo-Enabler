using Destiny2SoloEnabler.Models;
using NetFwTypeLib;
using System;
using System.Linq;

namespace Destiny2SoloEnabler.Service;

internal class FirewallService
{
    private Type _firewallPolicyType;
    private INetFwPolicy2 _firewallPolicy;

    public FirewallService()
    {
        _firewallPolicyType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
        _firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(_firewallPolicyType);
    }

    public static FirewallService Instance()
    {
        var service = new FirewallService();
        return service;
    }

    public bool DoesFirewallRuleExist(string ruleName)
    {
        bool ruleExists = _firewallPolicy.Rules
            .Cast<INetFwRule>()
            .Any(rule => rule.Name == ruleName);

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

        if (!String.IsNullOrEmpty(rule.ApplicationLocation))
        {
            inboundRule.ApplicationName = rule.ApplicationLocation;
        }
        

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

    /// <summary>
    /// Try and extract the value for the "ApplicationName" property of the given firewall rule
    /// <paramref name="appName"/> is null when rule matching "<paramref name="ruleName"/>" is not found
    /// </summary>
    /// <param name="ruleName">The name of the rule to look for.</param>
    /// <returns>A string. <para><c>String.Empty</c> if no application name could be found.</para><para><c>Name of the application for the rule</c> if a name can be extracted.</para></returns>
    public string ExtractApplicationNameFromRule(string ruleName)
    {
        string applicationName = String.Empty;

        foreach(INetFwRule2 rule in _firewallPolicy.Rules)
        {
            if(rule.Name == ruleName)
            {
                object objValue = rule.ApplicationName;

                if (objValue is null)
                {
                    continue;
                }

                applicationName = Convert.ToString(objValue);
                return applicationName;
            }
        }

        return applicationName;
    }
}


// SoloplayService.Instance().DoesFirewallRuleExist("dsadas");
// SoloplayService.Instance().RemoveFirewallRule("dsadwa");
// SoloplayService.Instance().CreateFirewallRule(new FirewallRule() { /* ... */ });