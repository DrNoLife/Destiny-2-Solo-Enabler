using D2SE.Domain.Entities;
using MediatR;

namespace D2SE.Application.Features.Settings.Commands.Save;

public record SaveSettingsCommand(AppSettings Settings) : IRequest<bool>;
