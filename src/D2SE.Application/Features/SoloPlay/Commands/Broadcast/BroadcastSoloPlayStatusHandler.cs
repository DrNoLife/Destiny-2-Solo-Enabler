using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.SoloPlay.Queries.GetStatus;
using D2SE.Application.Messages;
using MediatR;
using System.Diagnostics;

namespace D2SE.Application.Features.SoloPlay.Commands.Broadcast;

public class BroadcastSoloPlayStatusHandler(ISender mediator) : IRequestHandler<BroadcastSoloPlayStatusCommand>
{
    private readonly ISender _mediator = mediator;

    public async Task Handle(BroadcastSoloPlayStatusCommand request, CancellationToken cancellationToken)
    {
        GetSoloPlayStatusQuery query = new();
        var status = await _mediator.Send(query, cancellationToken);

        var message = status.SoloPlayIsActive
            ? SoloPlayStatusChangedMessage.Active()
            : SoloPlayStatusChangedMessage.NotActive();

        WeakReferenceMessenger.Default.Send(message);
    }
}
