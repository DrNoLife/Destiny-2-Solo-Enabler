using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.Settings.Queries.GetSettingsValue;
using D2SE.Application.Features.SoloPlay.Queries.GetStatus;
using D2SE.UI.Messages;
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
    }

    public async Task InitializeAsync()
    {
        // Get current status of solo play.
        GetStatusQuery soloPlayStatus = new();
        IsSoloPlayActive = (await _mediatr.Send(soloPlayStatus)).SoloPlayIsActive;

        // Check if program should be on top.
        GetSettingsQuery query = new("AlwaysOnTop");
        bool shouldBeTopmost = await _mediatr.Send(query);
        System.Windows.Application.Current.MainWindow.Topmost = shouldBeTopmost;

        HandleHotkeyRegistration();
    }

    [RelayCommand]
    private void ShowAbout() => IsAboutDisplayed = true;

    [RelayCommand]
    private void ShowSettings() => IsSettingsDisplayed = true;

    [RelayCommand]
    private static void CloseApplication()
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void HandleHotkeyRegistration()
    {
        Console.WriteLine("Should register hotkeys in the future...");
    }

}
