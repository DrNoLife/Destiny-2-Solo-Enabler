using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettingsValue;

public record GetSettingsQuery(string SettingsName) : IRequest<bool>;
