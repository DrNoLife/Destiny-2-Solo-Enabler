using D2SE.Application.Features.Hotkeys.Commands.Register;
using D2SE.Application.Features.Settings.Queries.GetSettings;
using MediatR;

namespace D2SE.Application.Features.Hotkeys.Commands.Initialize;

public class InitializeHotkeysHandler(ISender mediatr) : IRequestHandler<InitializeHotkeysCommand>
{
    private readonly ISender _mediatr = mediatr;

    public async Task Handle(InitializeHotkeysCommand request, CancellationToken cancellationToken)
    {
        GetSettingsQuery settingsQuery = new();
        var appSettings = await _mediatr.Send(settingsQuery, cancellationToken);

        if (appSettings.EnableHotkey)
        {
            HotkeyRegisterCommand registerCommand = new();
            await _mediatr.Send(registerCommand, cancellationToken);
        }
    }
}
