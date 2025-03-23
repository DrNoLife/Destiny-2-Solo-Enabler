using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettingsValue;

public class GetSettingsValueHandler(ISettingsService settingsService) : IRequestHandler<GetSettingsValueQuery, bool>
{
    private readonly ISettingsService _settingsService = settingsService;

    public Task<bool> Handle(GetSettingsValueQuery request, CancellationToken cancellationToken)
    {
        bool settingsValue = _settingsService.GetSettingsValue(request.SettingsName);
        return Task.FromResult(settingsValue);
    }
}
