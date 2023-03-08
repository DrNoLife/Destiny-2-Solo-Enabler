using Destiny2SoloEnabler.Enums;
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
        SettingsService.SetSettingsValue(SettingsNames.AlwaysOnTop.ToString(), AlwaysOnTop.IsChecked.ToString());
        SettingsService.SetSettingsValue(SettingsNames.EnableHotkey.ToString(), EnableHotkey.IsChecked.ToString());
        SettingsService.SetSettingsValue(SettingsNames.PersistantRules.ToString(), PersistantRules.IsChecked.ToString());

        // When done with saving, close the page.
        CloseAboutPage(sender, e);
    }

    private void SetSettingsValuesFromStore()
    {
        AlwaysOnTop.IsChecked = SettingsService.GetSettingsBooleanValue(SettingsNames.AlwaysOnTop.ToString());
        EnableHotkey.IsChecked = SettingsService.GetSettingsBooleanValue(SettingsNames.EnableHotkey.ToString());
        PersistantRules.IsChecked = SettingsService.GetSettingsBooleanValue(SettingsNames.PersistantRules.ToString());
    }
}
