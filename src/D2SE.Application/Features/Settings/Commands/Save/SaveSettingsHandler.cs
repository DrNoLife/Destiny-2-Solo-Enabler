using D2SE.Application.Features.Hotkeys.Commands.Register;
using D2SE.Application.Features.Hotkeys.Commands.Unregister;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public class SaveSettingsHandler(ISettingsService settingsService, ISender mediatr) : IRequestHandler<SaveSettingsCommand, bool>
{
    private readonly ISettingsService _settingsService = settingsService;
    private readonly ISender _mediatr = mediatr;   

    public async Task<bool> Handle(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _settingsService.SetSettingsValue(SettingsNames.AlwaysOnTop.ToString(), request.Settings.AlwaysOnTop.ToString());
            _settingsService.SetSettingsValue(SettingsNames.EnableHotkey.ToString(), request.Settings.EnableHotkey.ToString());
            _settingsService.SetSettingsValue(SettingsNames.PersistentRules.ToString(), request.Settings.PersistentRules.ToString());

            await HandleHotkeyRegistration(request, cancellationToken);

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
