using D2SE.Domain.Interfaces.Infrastructure;
using D2SE.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace D2SE.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IFirewallService, FirewallService>();
        services.AddSingleton<IHotkeyService, HotkeyService>();

        return services;
    }
}
