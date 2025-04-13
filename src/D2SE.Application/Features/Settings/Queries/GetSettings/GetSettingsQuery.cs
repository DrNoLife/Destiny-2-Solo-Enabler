using D2SE.Domain.Entities;
using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettings;

public record GetSettingsQuery : IRequest<AppSettings>;
