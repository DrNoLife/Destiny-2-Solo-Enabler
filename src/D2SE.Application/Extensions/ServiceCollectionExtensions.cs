using D2SE.Application.Features.Hotkeys.Services;
using D2SE.Application.Features.Settings.Queries.GetSettingsValue;
using D2SE.Domain.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;

namespace D2SE.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssemblies(typeof(GetSettingsValueQuery).Assembly));

        services.AddSingleton<IHotkeyNotification, HotkeyNotificationHandler>();

        return services;
    }
}
