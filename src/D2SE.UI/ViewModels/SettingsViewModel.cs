using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.Settings.Commands.Save;
using D2SE.Application.Features.Settings.Queries.GetSettings;
using D2SE.Domain.Entities;
using D2SE.Application.Messages;
using MediatR;
using D2SE.Application.Features.SoloPlay.Dtos;
using D2SE.Application.Features.SoloPlay.Commands.Toggle;
using D2SE.Application.Features.SoloPlay.Queries.GetStatus;

namespace D2SE.UI.ViewModels;

public partial class SettingsViewModel(ISender mediatr) : ObservableObject
{
    private readonly ISender _mediatr = mediatr;

    [ObservableProperty]
    private AppSettings _settings = AppSettings.CreateDefaultSettings();

    public async Task InitializeAsync()
    {
        Settings = await _mediatr.Send(new GetSettingsQuery());
    }

    [RelayCommand]
    public async Task CloseAndSaveAsync()
    {
        await _mediatr.Send(new SaveSettingsCommand(Settings));

        System.Windows.Application.Current.MainWindow.Topmost = Settings.AlwaysOnTop;

        // Get the status of the program, which forces the frontend UI to update if the invert functionality has changed.
        _ = await _mediatr.Send(new GetSoloPlayStatusQuery());

        SendCloseRequest();
    }

    [RelayCommand]
    public static void CloseWithoutSaving()
        => SendCloseRequest();

    private static void SendCloseRequest()
        => WeakReferenceMessenger.Default.Send(ClosePageMessage.CloseSettingsPage());
}
