using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using D2SE.UI.Messages;
using System.Diagnostics;

namespace D2SE.UI.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    [RelayCommand]
    public static void ClosePage()
    {
        WeakReferenceMessenger.Default.Send(ClosePageMessage.CloseAboutPage());
    }

    [RelayCommand]
    public void OpenGitHubRepository()
    {
        string url = "https://github.com/DrNoLife/Destiny-2-Solo-Enabler";
        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
    }
}
