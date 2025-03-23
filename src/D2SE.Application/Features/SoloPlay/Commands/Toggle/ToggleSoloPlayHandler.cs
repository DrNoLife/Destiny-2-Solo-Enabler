using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Application.Features.SoloPlay.Queries.GetStatus;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Toggle;

public class ToggleSoloPlayHandler(IFirewallService firewallService, ISender mediator) : IRequestHandler<ToggleSoloPlayCommand>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISender _mediator = mediator;

    public async Task Handle(ToggleSoloPlayCommand request, CancellationToken cancellationToken)
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

        await _mediator.Send(new BroadcastSoloPlayStatusCommand(!rulesActive), cancellationToken);

        //return await _mediator.Send(new GetSoloPlayStatusQuery(), cancellationToken);
    }
}
