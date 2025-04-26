using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Application;
using D2SE.Domain.Interfaces.Infrastructure;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;

namespace D2SE.Infrastructure.Services;

public class HotkeyService(IHotkeyNotification hotkeyNotification, IAlertService alertService) : IHotkeyService
{
    private readonly IHotkeyNotification _hotkeyNotification = hotkeyNotification;
    private readonly IAlertService _alertService = alertService;

    public void RegisterHotkeys()
    {
        try
        {
            HotkeyManager.Current.AddOrReplace(
            Hotkeys.ToggleSoloPlay.ToString(),
            Key.K, ModifierKeys.Alt | ModifierKeys.Shift,
            async (sender, e) => await OnHotkeyPressed(sender, e));
        }
        catch (HotkeyAlreadyRegisteredException ex) 
        {
            _alertService.ShowAlert("Failed to register hotkey", ex.Message, true);
        }
    }

    public void UnregisterHotkeys()
    {
        HotkeyManager.Current.Remove(Hotkeys.ToggleSoloPlay.ToString());
    }

    private async Task OnHotkeyPressed(object? sender, HotkeyEventArgs e)
    {
        await _hotkeyNotification.OnHotkeyPressed(e.Name);
        e.Handled = true;
    }
}
