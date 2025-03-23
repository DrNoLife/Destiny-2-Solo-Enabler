using CommunityToolkit.Mvvm.DependencyInjection;
using D2SE.UI.ViewModels;
using System.Windows;

namespace D2SE.UI.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeResources();

        var vm = Ioc.Default.GetService<MainWindowViewModel>();

        DataContext = vm;
        Loaded += async (s, e) 
            => await vm!.InitializeAsync();
    }

    private static void InitializeResources()
    {
        var dict = System.Windows.Application.LoadComponent(new Uri("Resources/StyleDictionary.xaml", UriKind.Relative)) as ResourceDictionary;
        if (dict is not null)
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }
}