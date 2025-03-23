using CommunityToolkit.Mvvm.DependencyInjection;
using D2SE.Application.Extensions;
using D2SE.Domain.Interfaces.Infrastructure;
using D2SE.Infrastructure.Services;
using D2SE.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace D2SE.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public App()
    {
        ServiceCollection services = new ();

        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IFirewallService, FirewallService>();

        services.AddApplicationServices();

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<AboutViewModel>();

        // Configure the default IoC container.
        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }
}
