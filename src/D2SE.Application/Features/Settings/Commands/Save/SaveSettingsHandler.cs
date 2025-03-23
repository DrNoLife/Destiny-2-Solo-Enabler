using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public class SaveSettingsHandler(ISettingsService settingsService) : IRequestHandler<SaveSettingsCommand>
{
    private readonly ISettingsService _settingsService = settingsService;

    public Task Handle(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        _settingsService.SetSettingsValue(request.settingsName, request.settingsValue);
        return Task.CompletedTask;
    }
}
