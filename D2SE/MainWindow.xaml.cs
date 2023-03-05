using D2SoloEnabler.Helpers;
using System;
using System.Windows;
using mrousavy;
using System.Windows.Input;

namespace D2SoloEnabler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string fwRuleName = "Destiny 2 - Solo-Enabler";
        static readonly string portRangeToBlock = "27000-27200,3097";

        public static readonly DependencyProperty IsAboutDisplayedProperty =
            DependencyProperty.Register("IsAboutDisplayed", typeof(bool), typeof(MainWindow),
            new PropertyMetadata(false));

        public static readonly DependencyProperty IsSettingsDisplayedProperty =
            DependencyProperty.Register("IsSettingsDisplayed", typeof(bool), typeof(MainWindow),
            new PropertyMetadata(false));

        public static readonly DependencyProperty IsSoloPlayActiveProperty =
            DependencyProperty.Register("IsSoloPlayActive", typeof(bool), typeof(MainWindow),
            new PropertyMetadata(false, OnPropertyIsSoloPlayActiveChanged));

        /// <summary>
        /// Indicates if we are initializing the IsSoloPlayActive value or not.
        /// </summary>
        private bool _initializing;

        /// <summary>
        /// Sets whether the about dialog is displayed or not.
        /// </summary>
        public bool IsAboutDisplayed
        {
            get { return (bool)GetValue(IsAboutDisplayedProperty); }
            set { SetValue(IsAboutDisplayedProperty, value); }
        }
        
        /// <summary>
        /// Sets whether the settings dialog is displayed or not.
        /// </summary>
        public bool IsSettingsDisplayed 
        {
            get => (bool)GetValue(IsSettingsDisplayedProperty); 
            set => SetValue(IsSettingsDisplayedProperty, value);
        }

        /// <summary>
        /// Sets whether the firewall rules exists or not.
        /// </summary>
        public bool IsSoloPlayActive
        {
            get { return (bool)GetValue(IsSoloPlayActiveProperty); }
            set { SetValue(IsSoloPlayActiveProperty, value); }
        }

        private bool _enableHotkey = false;
        private HotKey _soloEnablerHotkey = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeResources();
            
            DataContext = this;
            
            _initializing = true;
            IsSoloPlayActive = Soloplay.DoesFWRuleExist(fwRuleName);
            _initializing = false;

            // Makes sure the application has the highest z-index of all applications, thus doing the always-on-top.
            Topmost = Convert.ToBoolean(SettingsStore.GetSettingValue("AlwaysOnTop"));
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HandleHotkeyRegistration();
        }

        private void RemoveHotkey()
        {
            if(_soloEnablerHotkey != null)
            {
                _soloEnablerHotkey.Dispose();
                _soloEnablerHotkey = null;
            }
        }

        private void HandleHotkeyRegistration()
        {
            // Find out if we need to have hotkey functionality.
            _enableHotkey = Convert.ToBoolean(SettingsStore.GetSettingValue("EnableHotkey"));

            // If user wants to use hotkey, and one hasn't already been set, then create one.
            if (_enableHotkey && _soloEnablerHotkey == null)
            {
                try
                {
                    _soloEnablerHotkey = new HotKey((ModifierKeys.Alt | ModifierKeys.Shift), Key.K, this, (hotkey) =>
                    {
                        IsSoloPlayActive = !IsSoloPlayActive;
                    });
                }
                catch(Exception)
                {
                    // This was an issue earlier on development, but it should be fixed now... Buuut I still want to include this, just in case it ain't.
                    MessageBox.Show("Error initializing the hotkey. Create an issue on Github if this appears.");
                }
            }
            else
            {
                RemoveHotkey();
            }
        }

        private void InitializeResources()
        {
            // Load project-specific resources
            var dict = Application.LoadComponent(new Uri("Resources/Resources.xaml", UriKind.Relative)) as ResourceDictionary;
            if (dict != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
        }

        private void OnAboutCloseButtonClicked(object sender, EventArgs e) => IsAboutDisplayed = false;

        private void OnButtonAboutClicked(object sender, RoutedEventArgs e) => IsAboutDisplayed = true;

        /// <summary>
        /// Gets called whenever the user clicks on the settings button.
        /// Displays the settings dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsButtonClicked(object sender, RoutedEventArgs e) => IsSettingsDisplayed = true;

        /// <summary>
        /// Gets called whenever the user clicks close for the settings dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsDialogClosed(object sender, EventArgs e)
        {
            IsSettingsDisplayed = false;

            // Makes sure the application has the highest z-index of all applications, thus doing the always-on-top.
            Topmost = Convert.ToBoolean(SettingsStore.GetSettingValue("AlwaysOnTop"));
            HandleHotkeyRegistration();
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonCloseClicked(object sender, RoutedEventArgs e)
        {
            RemoveHotkey();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Upon closing the window.
        /// Remove all firewalls, and raise the close event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            RemoveHotkey();

            // Remove the FW rules before closing the application.
            Soloplay.RemoveFirewallRule(fwRuleName);

            base.OnClosed(e);
        }

        private static void OnPropertyIsSoloPlayActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MainWindow instance)
            {
                instance.OnIsSoloPlayActiveChanged();
            }
        }

        private void OnIsSoloPlayActiveChanged()
        {
            if(_initializing)
                return;

            // If the rule is indeed active. Remove FW rule. Check if still is active. Then do UI changed based on that.
            if (IsSoloPlayActive)
            {
                // Name. Portrange. Outbound yes/no. UDP yes/no.
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: true, isUDP: true);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: true, isUDP: false);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: false, isUDP: true);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: false, isUDP: false);
            }

            // If the rule is not active. We then add the FW rules. Check if they now exist. Then do UI changed based on that.
            else
            {
                Soloplay.RemoveFirewallRule(fwRuleName);
                IsSoloPlayActive = Soloplay.DoesFWRuleExist(fwRuleName);
            }
        }
    }
}
