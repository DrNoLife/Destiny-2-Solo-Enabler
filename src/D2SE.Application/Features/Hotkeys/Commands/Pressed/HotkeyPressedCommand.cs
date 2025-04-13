using MediatR;

namespace D2SE.Application.Features.Hotkeys.Commands.Pressed;

public record HotkeyPressedCommand(string HotkeyId) : IRequest;