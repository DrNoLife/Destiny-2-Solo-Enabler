using D2SE.Domain.Entities;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettings;

public class GetSettingsHandler(ISettingsService settingsService) : IRequestHandler<GetSettingsQuery, AppSettings>
{
    private readonly ISettingsService _settingsService = settingsService;

    public Task<AppSettings> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        AppSettings settings = new(
            _settingsService.GetSettingsValue(SettingsNames.AlwaysOnTop.ToString()),
            _settingsService.GetSettingsValue(SettingsNames.EnableHotkey.ToString()),
            _settingsService.GetSettingsValue(SettingsNames.PersistentRules.ToString()),
            _settingsService.GetSettingsValue(SettingsNames.InvertFunctionality.ToString()));

        return Task.FromResult(settings);
    }
}
