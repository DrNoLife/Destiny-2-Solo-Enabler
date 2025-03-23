using D2SE.Domain.Enums;
using MediatR;

namespace D2SE.Application.Features.Settings.Queries.GetSettingsValue;

public record GetSettingsValueQuery(string SettingsName) : IRequest<bool>
{
    public static GetSettingsValueQuery FromSetting(SettingsNames setting)
        => new(setting.ToString());
}
