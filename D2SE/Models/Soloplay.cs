using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace D2SoloEnabler
{
    class Soloplay
    {
        /// <summary>
        /// Returns either true or false, dependent on whether or not a rule with the supplied name exists.
        /// </summary>
        /// <param name="ruleName">The name of the rule to search for.</param>
        /// <returns>true if rule exists, otherwise false.</returns>
        public static bool DoesFWRuleExist(string ruleName)
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);

            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name == ruleName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates a Firewall rule. Use parameters to control name, port, direction and protocol.
        /// </summary>
        /// <param name="ruleName">Name of the rule.</param>
        /// <param name="portValue">Single port: "222". Range: "222-444".</param>
        /// <param name="isOut">true = outbound rule. False = inbound rule.</param>
        /// <param name="isUDP">true = UDP. False = TCP.</param>
        public static void CreateFWRule(string ruleName = "Firewall testing via C#", string portValue = "8000-8005", bool isOut = true, bool isUDP = true)
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            // Let's create a new FW rule.
            INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            // Block it through firewall
            inboundRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            // Quick description for the rule.
            inboundRule.Description = "Used by the program Solo Enabler. Its use is to enable solo play in Destiny 2.";

            // Set the direction.
            inboundRule.Direction = isOut ? NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT : NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;

            // Make sure rule is enabled.
            inboundRule.Enabled = true;

            // Set the name of the FW rule
            inboundRule.Name = ruleName;

            // Make sure to set the protocol before the ports. TCP = 6. UDP = 17.
            inboundRule.Protocol = (int)(isUDP ? NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP : NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);

            // Set the ports.
            inboundRule.RemotePorts = portValue;

            // Add the rule itself
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Add(inboundRule);
        }

        /// <summary>
        /// Removes firewall rule, if one with the supplied name exists.
        /// </summary>
        /// <param name="ruleName">Name of firewall rule to remove.</param>
        public static void RemoveFirewallRule(string ruleName)
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);

            // Go through the rule list
            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name == ruleName)
                {
                    fwPolicy2.Rules.Remove(ruleName);               // If a match is found. Then delete the rule with our supplied name.
                }
            }
        }
    }
}
