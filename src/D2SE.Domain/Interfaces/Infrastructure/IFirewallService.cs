namespace D2SE.Domain.Interfaces.Infrastructure;

public interface IFirewallService
{
    bool FirewallRulesExists();
    void RemoveFirewallRules();
    void CreateFirewallRules();
}
