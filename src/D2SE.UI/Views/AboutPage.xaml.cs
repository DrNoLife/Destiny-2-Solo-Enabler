using CommunityToolkit.Mvvm.DependencyInjection;
using D2SE.UI.ViewModels;
using System.Windows.Controls;

namespace D2SE.UI.Views;

/// <summary>
/// Interaction logic for AboutPage.xaml
/// </summary>
public partial class AboutPage : UserControl
{
    public AboutPage()
    {
        InitializeComponent();

        var vm = Ioc.Default.GetService<AboutViewModel>();

        DataContext = vm;
    }
}
