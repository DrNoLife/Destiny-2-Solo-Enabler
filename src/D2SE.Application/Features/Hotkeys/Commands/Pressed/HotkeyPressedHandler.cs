using Enums = D2SE.Domain.Enums;
using MediatR;
using D2SE.Application.Features.SoloPlay.Commands.Toggle;

namespace D2SE.Application.Features.Hotkeys.Commands.Pressed;

public class HotkeyPressedHandler(IMediator mediator) : IRequestHandler<HotkeyPressedCommand>
{
    private readonly IMediator _mediator = mediator;

    public async Task Handle(HotkeyPressedCommand request, CancellationToken cancellationToken)
    {
        if (!request.HotkeyId.Equals(Enums.Hotkeys.ToggleSoloPlay.ToString()))
        {
            throw new Exception("Unknown hotkey");
        }

        await _mediator.Send(new ToggleSoloPlayCommand(), cancellationToken);
    }
}
