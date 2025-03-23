using D2SE.Application.Features.Settings.Queries.GetSettingsValue;
using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Queries.GetStatus;

public class GetSoloPlayStatusHandler(IFirewallService firewallService, ISender mediator) : IRequestHandler<GetSoloPlayStatusQuery, SoloPlayStatusDto>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISender _mediator = mediator;  

    public async Task<SoloPlayStatusDto> Handle(GetSoloPlayStatusQuery request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        bool invertBehaviour = await _mediator.Send(GetSettingsValueQuery.FromSetting(SettingsNames.InvertFunctionality), cancellationToken);

        if (invertBehaviour)
        {
            rulesActive = !rulesActive;
        }

        var command = rulesActive
            ? BroadcastSoloPlayStatusCommand.Activated()
            : BroadcastSoloPlayStatusCommand.Deactivated();

        await _mediator.Send(command, cancellationToken);

        return new SoloPlayStatusDto(rulesActive);
    }
}
