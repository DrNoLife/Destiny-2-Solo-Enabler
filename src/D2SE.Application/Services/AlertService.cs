using D2SE.Domain.Interfaces.Infrastructure;
using D2SE.Domain.Enums;
using Microsoft.Toolkit.Uwp.Notifications;

namespace D2SE.Application.Services;

public class AlertService(ISettingsService settingsService) : IAlertService
{
    private readonly ISettingsService _settingsService = settingsService;

    public void ShowAlert(string message, bool forceAlert = false)
        => ShowAlert(String.Empty, message, forceAlert);

    public void ShowAlert(string title, string message, bool forceAlert = false)
    {
        ToastContentBuilder builder = new();

        if (!String.IsNullOrEmpty(title))
        {
            builder.AddText(title);
        }

        builder.AddText(message);

        var userNotificationsSaved = _settingsService.CheckIfSettingExists(SettingsNames.EnableNotifications);
        var userHasNotificationsEnabled = _settingsService.GetSettingsValue<bool>(SettingsNames.EnableNotifications);

        if (userNotificationsSaved && !userHasNotificationsEnabled && !forceAlert)
        {
            return;
        }

        builder.Show();
    }
}
