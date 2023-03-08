using Destiny2SoloEnabler.Enums;
using Destiny2SoloEnabler.Models;
using Destiny2SoloEnabler.Service;
using mrousavy;
using System;
using System.Windows;
using System.Windows.Input;

namespace Destiny2SoloEnabler;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // Public static
    public static readonly DependencyProperty IsAboutDisplayedProperty = 
        DependencyProperty.Register("IsAboutDisplayed", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

    public static readonly DependencyProperty IsSettingsDisplayedProperty =
        DependencyProperty.Register("IsSettingsDisplayed", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

    public static readonly DependencyProperty IsSoloPlayActiveProperty =
        DependencyProperty.Register("IsSoloPlayActive", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnPropertyIsSoloPlayActiveChanged));

    // Public properties
    public bool IsAboutDisplayed 
    {
        get => (bool)GetValue(IsAboutDisplayedProperty);
        set => SetValue(IsAboutDisplayedProperty, value);
    }

    public bool IsSettingsDisplayed 
    { 
        get => (bool)GetValue(IsSettingsDisplayedProperty); 
        set => SetValue(IsSettingsDisplayedProperty, value); 
    }

    public bool IsSoloPlayActive 
    {
        get => !((bool)GetValue(IsSoloPlayActiveProperty)); 
        set => SetValue(IsSoloPlayActiveProperty, value);
    }

    public int Height => Helpers.Constants.Height;
    public int Width => Helpers.Constants.Width;

    // Private fields.
    private bool _initializing;
    private FirewallRule _firewallRule;
    private bool _enableHotkey = false;
    private HotKey _soloEnablerHotkey = null;

    public MainWindow()
    {
        InitializeComponent();
        InitializeResources();
        DataContext = this;

        _firewallRule = new();
        _firewallRule.PortValue = "27000-27200,3097";
        _firewallRule.RuleName = "Destiny 2 - Solo-Enabler";

        // If user closed the program before deleting the rules, then make sure to reflect that in the view.
        _initializing = true;
        IsSoloPlayActive = SoloPlayService.Instance().DoesFirewallRuleExist(_firewallRule.RuleName);
        _initializing = false;

        // Makes sure the application has the highest z-index of all applications, thus doing the always-on-top.
        Topmost = Convert.ToBoolean(SettingsService.GetSettingsValue("AlwaysOnTop"));
    }

    private void InitializeResources()
    {
        // Load project-specific resources
        var dict = Application.LoadComponent(new Uri("Resources/StyleDictionary.xaml", UriKind.Relative)) as ResourceDictionary;
        if (dict != null)
        {
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        HandleHotkeyRegistration();
    }

    private void RemoveHotkey()
    {
        if (_soloEnablerHotkey != null)
        {
            _soloEnablerHotkey.Dispose();
            _soloEnablerHotkey = null;
        }
    }

    private void HandleHotkeyRegistration()
    {
        // Find out if we need to have hotkey functionality.
        _enableHotkey = Convert.ToBoolean(SettingsService.GetSettingsValue("EnableHotkey"));

        if(!_enableHotkey || _soloEnablerHotkey is not null)
        {
            RemoveHotkey();
            return;
        }

        try
        {
            _soloEnablerHotkey = new HotKey((ModifierKeys.Alt | ModifierKeys.Shift), Key.K, this, (hotkey) =>
            {
                IsSoloPlayActive = IsSoloPlayActive;
            });
        }
        catch (Exception)
        {
            // This was an issue earlier on development, but it should be fixed now... Buuut I still want to include this, just in case it ain't.
            MessageBox.Show("Error initializing the hotkey. Create an issue on Github if this appears.");
        }
    }

    private void OnAboutCloseButtonClicked(object sender, EventArgs e) => IsAboutDisplayed = false;
    private void OnButtonAboutClicked(object sender, EventArgs e) => IsAboutDisplayed = true;

    private void OnSettingsCloseButtonClicked(object sender, EventArgs e)
    {
        IsSettingsDisplayed = false;

        // Makes sure the application has the highest z-index of all applications, thus doing the always-on-top.
        Topmost = Convert.ToBoolean(SettingsService.GetSettingsValue("AlwaysOnTop"));
        HandleHotkeyRegistration();
    }
    private void OnButtonSettingsClicked(object sender, EventArgs e) => IsSettingsDisplayed = true;

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

        bool deleteRulesUponClosing = !SettingsService.GetSettingsBooleanValue(SettingsNames.PersistantRules.ToString());
        if (deleteRulesUponClosing)
        {
            SoloPlayService.Instance().RemoveFirewallRule(_firewallRule.RuleName);
        }

        base.OnClosed(e);
    }

    private static void OnPropertyIsSoloPlayActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if(dependencyObject is MainWindow instance)
        {
            instance.OnIsSoloPlayActiveChanged();
        }
    }

    private void OnIsSoloPlayActiveChanged()
    {
        if(_initializing)
        {
            return;
        }

        if(IsSoloPlayActive)
        {
            SoloPlayService.Instance().RemoveFirewallRule(_firewallRule.RuleName);
            IsSoloPlayActive = SoloPlayService.Instance().DoesFirewallRuleExist(_firewallRule.RuleName);
            return;
        }

        SoloPlayService.Instance().CreateFirewallRules(_firewallRule);
        IsSoloPlayActive = SoloPlayService.Instance().DoesFirewallRuleExist(_firewallRule.RuleName);
    }
}
