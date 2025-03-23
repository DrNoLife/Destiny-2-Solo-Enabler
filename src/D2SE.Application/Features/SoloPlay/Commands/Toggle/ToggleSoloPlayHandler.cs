using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Application.Messages;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Toggle;

public class ToggleSoloPlayHandler(IFirewallService firewallService) : IRequestHandler<ToggleSoloPlayCommand, SoloPlayStatusDto>
{
    private readonly IFirewallService _firewallService = firewallService;

    public Task<SoloPlayStatusDto> Handle(ToggleSoloPlayCommand request, CancellationToken cancellationToken)
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

        var message = !rulesActive
            ? SoloPlayStatusChangedMessage.Active()
            : SoloPlayStatusChangedMessage.NotActive();

        WeakReferenceMessenger.Default.Send(message);

        return Task.FromResult(new SoloPlayStatusDto(!rulesActive));
    }
}
