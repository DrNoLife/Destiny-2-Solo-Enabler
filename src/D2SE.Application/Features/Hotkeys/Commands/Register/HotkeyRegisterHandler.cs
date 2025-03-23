using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Hotkeys.Commands.Register;

public class HotkeyRegisterHandler(IHotkeyService hotkeyService) : IRequestHandler<HotkeyRegisterCommand>
{
    private readonly IHotkeyService _hotkeyService = hotkeyService;

    public Task Handle(HotkeyRegisterCommand request, CancellationToken cancellationToken)
    {
        _hotkeyService.RegisterHotkeys();
        return Task.CompletedTask;
    }
}
