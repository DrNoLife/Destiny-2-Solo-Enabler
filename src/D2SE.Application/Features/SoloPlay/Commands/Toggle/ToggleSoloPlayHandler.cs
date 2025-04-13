﻿using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Domain.Constants;
using D2SE.Domain.Entities;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;
using System.Diagnostics;

namespace D2SE.Application.Features.SoloPlay.Commands.Toggle;

public class ToggleSoloPlayHandler(IFirewallService firewallService, ISender mediator) : IRequestHandler<ToggleSoloPlayCommand>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISender _mediator = mediator;

    public async Task Handle(ToggleSoloPlayCommand request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        Debug.WriteLine($"Rules currently active: {rulesActive}");

        if (rulesActive)
        {
            _firewallService.RemoveFirewallRules();
            Debug.WriteLine("Want to remove rules");
        }
        else
        {
            FirewallRule ruleEntity = FirewallRule.CreateRule();

            var commandLineArgs = Environment.GetCommandLineArgs().ToList();
            var portRangeIndex = commandLineArgs.IndexOf("-PortRange");

            if (portRangeIndex != -1)
            {
                var portRange = commandLineArgs[portRangeIndex + 1];
                ruleEntity = ruleEntity with { PortValue = portRange };
            }

            _firewallService.CreateFirewallRules(ruleEntity);
            Debug.WriteLine("Want to add rules");
        }


        await _mediator.Send(new BroadcastSoloPlayStatusCommand(), cancellationToken);
    }
}
