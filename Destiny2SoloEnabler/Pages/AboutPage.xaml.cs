using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace Destiny2SoloEnabler.Pages;

/// <summary>
/// Interaction logic for AboutPage.xaml
/// </summary>
public partial class AboutPage : UserControl
{
    public event EventHandler OnClose;

    public AboutPage()
    {
        InitializeComponent();
    }

    private void CloseAboutPage(object sender, RoutedEventArgs e)
    {
        OnClose?.Invoke(this, EventArgs.Empty);
    }

    private void OpenGithubRepository(object sender, RoutedEventArgs e)
    {
        string url = "https://github.com/DrNoLife/Destiny-2-Solo-Enabler";
        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
    }
}
