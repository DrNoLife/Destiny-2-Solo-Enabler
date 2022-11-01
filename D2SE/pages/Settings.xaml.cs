using D2SoloEnabler.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace D2SoloEnabler.pages
{

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            // Initialize the settings with their proper stored value.
            AlwaysOnTop.IsChecked = Convert.ToBoolean(SettingsStore.GetSettingValue("AlwaysOnTop"));
            EnableHotkey.IsChecked = Convert.ToBoolean(SettingsStore.GetSettingValue("EnableHotkey"));
        }

        public event EventHandler Closed;

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            // Optimally we'd have a more procedual way of doing this, but honestly, meh.
            // Let's just go through the settings one-by-one (which is fine, cuz we've only got one settings... So ye. KISS).
            SettingsStore.SetSettingValue("AlwaysOnTop", AlwaysOnTop.IsChecked.ToString());
            SettingsStore.SetSettingValue("EnableHotkey", EnableHotkey.IsChecked.ToString());

            Closed?.Invoke(this, EventArgs.Empty);
        }
    }
}
