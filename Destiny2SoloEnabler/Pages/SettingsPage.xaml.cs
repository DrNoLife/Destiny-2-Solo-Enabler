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
        SettingsService.SetSettingsValue(KeyNames.AlwaysOnTop, AlwaysOnTop.IsChecked.ToString());
        SettingsService.SetSettingsValue(KeyNames.EnableHotkey, EnableHotkey.IsChecked.ToString());
        SettingsService.SetSettingsValue(KeyNames.PersistantRules, PersistantRules.IsChecked.ToString());
        SettingsService.SetSettingsValue(KeyNames.ToggleDestiny2Rules, ToggleDestiny2Rules.IsChecked.ToString());

        // When done with saving, close the page.
        CloseAboutPage(sender, e);
    }

    private void SetSettingsValuesFromStore()
    {
        AlwaysOnTop.IsChecked = SettingsService.GetSettingsBooleanValue(KeyNames.AlwaysOnTop);
        EnableHotkey.IsChecked = SettingsService.GetSettingsBooleanValue(KeyNames.EnableHotkey);
        PersistantRules.IsChecked = SettingsService.GetSettingsBooleanValue(KeyNames.PersistantRules);
        ToggleDestiny2Rules.IsChecked = SettingsService.GetSettingsBooleanValue(KeyNames.ToggleDestiny2Rules);
    }
}
