using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Disable;

public class DisableSoloPlayHandler(IFirewallService firewallService, ISender mediator) : IRequestHandler<DisableSoloPlayCommand>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISender _mediator = mediator;

    public async Task Handle(DisableSoloPlayCommand request, CancellationToken cancellationToken)
    {
        _firewallService.RemoveFirewallRules();
        await _mediator.Send(new BroadcastSoloPlayStatusCommand(), cancellationToken);
    }
}
