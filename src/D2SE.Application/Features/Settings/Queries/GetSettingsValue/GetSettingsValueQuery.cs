using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettingsValue;

public record GetSettingsValueQuery(string SettingsName) : IRequest<bool>;
