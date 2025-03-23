using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Messages;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Commands.Broadcast;

public class BroadcastSoloPlayStatusHandler : IRequestHandler<BroadcastSoloPlayStatusCommand>
{
    public Task Handle(BroadcastSoloPlayStatusCommand request, CancellationToken cancellationToken)
    {
        var message = request.IsActive
            ? SoloPlayStatusChangedMessage.Active()
            : SoloPlayStatusChangedMessage.NotActive();

        WeakReferenceMessenger.Default.Send(message);

        return Task.CompletedTask;
    }
}
