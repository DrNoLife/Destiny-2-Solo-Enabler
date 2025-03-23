using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Broadcast;

public record BroadcastSoloPlayStatusCommand(bool IsActive) : IRequest
{
    public static BroadcastSoloPlayStatusCommand Activated()
        => new(true);

    public static BroadcastSoloPlayStatusCommand Deactivated() 
        => new(false);
}
