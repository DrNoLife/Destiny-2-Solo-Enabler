using D2SE.Application.Features.SoloPlay.Dtos;
using MediatR;

namespace D2SE.Application.Features.SoloPlay.Queries.GetStatus;

public record GetSoloPlayStatusQuery() : IRequest<SoloPlayStatusDto>;
