using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.Hotkeys.Commands.Initialize;
using D2SE.Application.Features.Settings.Queries.GetSettingsValue;
using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Application.Features.SoloPlay.Commands.Disable;
using D2SE.Application.Features.SoloPlay.Commands.Toggle;
using D2SE.Application.Features.SoloPlay.Queries.GetStatus;
using D2SE.Application.Messages;
using D2SE.Domain.Enums;
using MediatR;

namespace D2SE.UI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ISender _mediatr;

    [ObservableProperty]
    private bool _isAboutDisplayed;

    [ObservableProperty]
    private bool _isSettingsDisplayed;

    [ObservableProperty]
    private bool _isSoloPlayActive;

    public MainWindowViewModel(ISender mediatr)
    {
        _mediatr = mediatr;

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
        GetSettingsValueQuery query = new("AlwaysOnTop");
        bool shouldBeTopmost = await _mediatr.Send(query);
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
        var shouldPersistRules = await _mediatr.Send(GetSettingsValueQuery.FromSetting(SettingsNames.PersistentRules));

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
