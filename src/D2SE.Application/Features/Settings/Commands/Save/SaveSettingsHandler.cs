using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public class SaveSettingsHandler(ISettingsService settingsService) : IRequestHandler<SaveSettingsCommand, bool>
{
    private readonly ISettingsService _settingsService = settingsService;

    public Task<bool> Handle(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _settingsService.SetSettingsValue(SettingsNames.AlwaysOnTop.ToString(), request.Settings.AlwaysOnTop.ToString());
            _settingsService.SetSettingsValue(SettingsNames.EnableHotkey.ToString(), request.Settings.EnableHotkey.ToString());
            _settingsService.SetSettingsValue(SettingsNames.PersistentRules.ToString(), request.Settings.PersistentRules.ToString());

            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}
