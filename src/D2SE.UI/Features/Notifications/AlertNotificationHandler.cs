using D2SE.Domain.Entities;
using MediatR;
using System.Windows;

namespace D2SE.UI.Features.Notifications;

public class AlertNotificationHandler : INotificationHandler<AlertNotification>
{
    public Task Handle(AlertNotification notification, CancellationToken cancellationToken)
    {
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            MessageBox.Show(notification.Message, notification.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        });
        return Task.CompletedTask;
    }
}
