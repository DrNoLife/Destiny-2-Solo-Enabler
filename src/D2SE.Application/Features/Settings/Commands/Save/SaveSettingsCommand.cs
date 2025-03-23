using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public record SaveSettingsCommand(string settingsName, string settingsValue) : IRequest;
