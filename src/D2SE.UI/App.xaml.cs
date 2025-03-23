using CommunityToolkit.Mvvm.DependencyInjection;
using D2SE.Application.Extensions;
using D2SE.Infrastructure.Extensions;
using D2SE.UI.Features.Notifications;
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

        services.AddApplicationServices();
        services.AddInfrastructureServices();

        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssemblies(typeof(AlertNotificationHandler).Assembly));

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<AboutViewModel>();

        // Configure the default IoC container.
        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }
}
