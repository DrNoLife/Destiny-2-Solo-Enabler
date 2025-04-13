using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Hotkeys.Commands.Unregister;

public class HotkeyUnregisterHandler(IHotkeyService hotkeyService) : IRequestHandler<HotkeyUnregisterCommand>
{
    private readonly IHotkeyService _hotkeyService = hotkeyService;

    public Task Handle(HotkeyUnregisterCommand request, CancellationToken cancellationToken)
    {
        _hotkeyService.UnregisterHotkeys();
        return Task.CompletedTask;
    }
}
