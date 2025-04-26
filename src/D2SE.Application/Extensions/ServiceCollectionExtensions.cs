using D2SE.Application.Features.Hotkeys.Services;
using D2SE.Application.Features.Settings.Queries.GetSettings;
using D2SE.Application.Services;
using D2SE.Domain.Interfaces.Application;
using D2SE.Domain.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace D2SE.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssemblies(typeof(GetSettingsQuery).Assembly));

        services.AddSingleton<IHotkeyNotification, HotkeyNotificationHandler>();
        services.AddSingleton<IAlertService, AlertService>();

        return services;
    }
}
