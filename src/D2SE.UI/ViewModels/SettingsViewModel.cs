using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.Application.Features.Settings.Queries.GetSettingsValue;
using D2SE.UI.Messages;
using MediatR;

namespace D2SE.UI.ViewModels;

public partial class SettingsViewModel(ISender mediatr) : ObservableObject
{
    private readonly ISender _mediatr = mediatr;

    [RelayCommand]
    public async Task CloseAndSaveAsync()
    {
        // Save settings via MediatR or your settings service
        GetSettingsQuery query = new("AlwaysOnTop");
        bool shouldBeTopmost = await _mediatr.Send(query);
        System.Windows.Application.Current.MainWindow.Topmost = shouldBeTopmost;

        // Register or update hotkeys if necessary
        // ...

        SendCloseRequest();
    }

    [RelayCommand]
    public void CloseWithoutSaving()
        => SendCloseRequest();

    private static void SendCloseRequest()
        => WeakReferenceMessenger.Default.Send(ClosePageMessage.CloseSettingsPage());
}
