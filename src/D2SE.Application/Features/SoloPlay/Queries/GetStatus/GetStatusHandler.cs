using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Queries.GetStatus;

public class GetStatusHandler(IFirewallService firewallService) : IRequestHandler<GetStatusQuery, SoloPlayStatusDto>
{
    private readonly IFirewallService _firewallService = firewallService;

    public Task<SoloPlayStatusDto> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        return Task.FromResult(new SoloPlayStatusDto(rulesActive));
    }
}
