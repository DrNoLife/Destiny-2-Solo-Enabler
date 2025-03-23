using Enums = D2SE.Domain.Enums;
using MediatR;
using D2SE.Application.Features.SoloPlay.Commands.Toggle;

namespace D2SE.Application.Features.Hotkeys.Commands.Pressed;

public class HotkeyPressedHandler(ISender mediatr) : IRequestHandler<HotkeyPressedCommand>
{
    private readonly ISender _mediatr = mediatr;

    public async Task Handle(HotkeyPressedCommand request, CancellationToken cancellationToken)
    {
        if (!request.HotkeyId.Equals(Enums.Hotkeys.ToggleSoloPlay.ToString()))
        {
            throw new Exception("Unknown hotkey");
        }

        await _mediatr.Send(new ToggleSoloPlayCommand(), cancellationToken);
    }
}
