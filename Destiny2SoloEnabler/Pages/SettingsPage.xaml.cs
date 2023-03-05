using Destiny2SoloEnabler.Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Destiny2SoloEnabler.Pages;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : UserControl
{
    public event EventHandler OnClose;

    public SettingsPage()
    {
        InitializeComponent();

        // Initialize the settings with their stored values.
        SetSettingsValuesFromStore();
    }

    private void CloseAboutPage(object sender, RoutedEventArgs e)
    {
        // Prevent data being stored across open / close of the page.
        SetSettingsValuesFromStore();

        OnClose?.Invoke(this, EventArgs.Empty);
    }

    private void OnSaveClick(object sender, RoutedEventArgs e)
    {
        // Save the settings.
        SettingsService.SetSettingsValue("AlwaysOnTop", AlwaysOnTop.IsChecked.ToString());
        SettingsService.SetSettingsValue("EnableHotkey", EnableHotkey.IsChecked.ToString());

        // When done with saving, close the page.
        CloseAboutPage(sender, e);
    }

    private void SetSettingsValuesFromStore()
    {
        AlwaysOnTop.IsChecked = Convert.ToBoolean(SettingsService.GetSettingsValue("AlwaysOnTop"));
        EnableHotkey.IsChecked = Convert.ToBoolean(SettingsService.GetSettingsValue("EnableHotkey"));
    }
}
