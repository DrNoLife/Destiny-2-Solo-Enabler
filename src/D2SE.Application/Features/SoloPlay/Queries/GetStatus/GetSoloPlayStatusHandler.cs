using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Queries.GetStatus;

public class GetSoloPlayStatusHandler(IFirewallService firewallService, ISettingsService settingsService) : IRequestHandler<GetSoloPlayStatusQuery, SoloPlayStatusDto>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISettingsService _settingsService = settingsService;

    public Task<SoloPlayStatusDto> Handle(GetSoloPlayStatusQuery request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        bool shouldInvertBehaviour = _settingsService.GetSettingsValue<bool>(SettingsNames.InvertFunctionality);

        if (shouldInvertBehaviour)
        {
            rulesActive = !rulesActive;
        }

        return Task.FromResult(new SoloPlayStatusDto(rulesActive));
    }
}
