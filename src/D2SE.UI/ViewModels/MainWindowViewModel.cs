using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.Hotkeys.Commands.Initialize;
using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Application.Features.SoloPlay.Commands.Disable;
using D2SE.Application.Features.SoloPlay.Commands.Toggle;
using D2SE.Application.Messages;
using D2SE.Domain.Enums;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;

namespace D2SE.UI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ISender _mediatr;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private bool _isAboutDisplayed;

    [ObservableProperty]
    private bool _isSettingsDisplayed;

    [ObservableProperty]
    private bool _isSoloPlayActive;

    public MainWindowViewModel(ISender mediatr, ISettingsService settingsService)
    {
        _mediatr = mediatr;
        _settingsService = settingsService;

        WeakReferenceMessenger.Default.Register<ClosePageMessage>(this, (r ,m) =>
        {
            if (m.Value == "About")
            {
                IsAboutDisplayed = false;
            }
            else if (m.Value == "Settings")
            {
                IsSettingsDisplayed = false;
            }
        });

        WeakReferenceMessenger.Default.Register<SoloPlayStatusChangedMessage>(this, (r, message) =>
        {
            IsSoloPlayActive = message.IsActive;
        });
    }

    public async Task InitializeAsync()
    {
        await _mediatr.Send(new BroadcastSoloPlayStatusCommand());

        // Check if program should be on top.
        bool shouldBeTopmost = _settingsService.GetSettingsValue<bool>(SettingsNames.AlwaysOnTop);
        System.Windows.Application.Current.MainWindow.Topmost = shouldBeTopmost;

        await HandleHotkeyRegistration();
    }

    private async Task HandleHotkeyRegistration()
    {
        InitializeHotkeysCommand command = new();
        await _mediatr.Send(command);
    }

    [RelayCommand]
    private void ShowAbout() => IsAboutDisplayed = true;

    [RelayCommand]
    private void ShowSettings() => IsSettingsDisplayed = true;

    [RelayCommand]
    private async Task CloseApplication()
    {
        var shouldPersistRules = _settingsService.GetSettingsValue<bool>(SettingsNames.PersistentRules);

        if (!shouldPersistRules)
        {
            await _mediatr.Send(new DisableSoloPlayCommand());
        }

        System.Windows.Application.Current.Shutdown();
    }

    [RelayCommand]
    public async Task ToggleSoloPlay()
    {
         await _mediatr.Send(new ToggleSoloPlayCommand());
    }
}
