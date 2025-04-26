using D2SE.Application.Features.Hotkeys.Commands.Register;
using D2SE.Application.Features.Hotkeys.Commands.Unregister;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public class SaveSettingsHandler(ISettingsService settingsService, ISender mediatr, IAlertService alertService) : IRequestHandler<SaveSettingsCommand, bool>
{
    private readonly ISettingsService _settingsService = settingsService;
    private readonly ISender _mediatr = mediatr;   
    private readonly IAlertService _alertService = alertService;

    public async Task<bool> Handle(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _settingsService.SetSettingsValue(SettingsNames.AlwaysOnTop, request.Settings.AlwaysOnTop.ToString());
            _settingsService.SetSettingsValue(SettingsNames.EnableHotkey, request.Settings.EnableHotkey.ToString());
            _settingsService.SetSettingsValue(SettingsNames.PersistentRules, request.Settings.PersistentRules.ToString());
            _settingsService.SetSettingsValue(SettingsNames.InvertFunctionality, request.Settings.InvertFunctionality.ToString());
            _settingsService.SetSettingsValue(SettingsNames.EnableNotifications, request.Settings.EnableNotifications.ToString());
            _settingsService.SetSettingsValue(SettingsNames.OverridePortRange, request.Settings.OverridePortsToBlock.ToString());
            _settingsService.SetSettingsValue(SettingsNames.CustomPortRange, request.Settings.CustomPortRangeToBlock.ToString());

            await HandleHotkeyRegistration(request, cancellationToken);

            _alertService.ShowAlert("Settings saved");

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task HandleHotkeyRegistration(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        if (request.Settings.EnableHotkey)
        {
            HotkeyRegisterCommand hotkeyRegisterCommand = new();
            await _mediatr.Send(hotkeyRegisterCommand, cancellationToken);
        }
        else
        {
            HotkeyUnregisterCommand hotkeyUnregisterCommand = new();
            await _mediatr.Send(hotkeyUnregisterCommand, cancellationToken);
        }
    }
}
