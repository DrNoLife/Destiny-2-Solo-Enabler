using CommunityToolkit.Mvvm.DependencyInjection;
using D2SE.UI.ViewModels;
using System.Windows.Controls;

namespace D2SE.UI.Views;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : UserControl
{
    public SettingsPage()
    {
        InitializeComponent();

        var vm = Ioc.Default.GetService<SettingsViewModel>();

        DataContext = vm;
        Loaded += async (s, e)
            => await vm!.InitializeAsync();
    }
}
