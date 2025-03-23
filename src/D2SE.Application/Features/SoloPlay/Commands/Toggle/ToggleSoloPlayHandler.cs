using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Toggle;

public class ToggleSoloPlayHandler(IFirewallService firewallService) : IRequestHandler<ToggleSoloPlayCommand>
{
    private readonly IFirewallService _firewallService = firewallService;

    public Task Handle(ToggleSoloPlayCommand request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        if (rulesActive)
        {
            _firewallService.RemoveFirewallRules();
        }
        else
        {
            _firewallService.CreateFirewallRules();
        }

        return Task.CompletedTask;
    }
}
