using D2SE.Application.Features.Hotkeys.Commands.Pressed;
using D2SE.Domain.Interfaces.Application;
using MediatR;

namespace D2SE.Application.Features.Hotkeys.Services;

public class HotkeyNotificationHandler(ISender mediatr) : IHotkeyNotification
{
    private readonly ISender _mediatr = mediatr;

    public async Task OnHotkeyPressed(string hotkeyId)
    {
        await _mediatr.Send(new HotkeyPressedCommand(hotkeyId));
    }
}
