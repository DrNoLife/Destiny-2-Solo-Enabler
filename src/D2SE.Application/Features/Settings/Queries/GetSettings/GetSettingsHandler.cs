using D2SE.Domain.Constants;
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
        string portRangeToBlock = _settingsService.GetSettingsValue<string>(SettingsNames.CustomPortRange);

        if (String.IsNullOrEmpty(portRangeToBlock))
        {
            portRangeToBlock = D2SEConstants.PortRange;
        }

        bool enableNotifications = _settingsService.CheckIfSettingExists(SettingsNames.EnableNotifications)
            ? _settingsService.GetSettingsValue<bool>(SettingsNames.EnableNotifications)
            : true;

        AppSettings settings = new(
            _settingsService.GetSettingsValue<bool>(SettingsNames.AlwaysOnTop),
            _settingsService.GetSettingsValue<bool>(SettingsNames.EnableHotkey),
            _settingsService.GetSettingsValue<bool>(SettingsNames.PersistentRules),
            _settingsService.GetSettingsValue<bool>(SettingsNames.InvertFunctionality),
            enableNotifications,
            _settingsService.GetSettingsValue<bool>(SettingsNames.OverridePortRange),
            portRangeToBlock);

        return Task.FromResult(settings);
    }
}
