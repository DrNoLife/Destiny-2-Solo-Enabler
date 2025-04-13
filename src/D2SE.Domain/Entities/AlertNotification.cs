using MediatR;

namespace D2SE.Domain.Entities;

public record AlertNotification(string Title, string Message) : INotification
{
    public static AlertNotification NewAlert(string title, string message)
        => new(title, message);

    public static AlertNotification NewAlert(string message)
        => new("Alert", message);
}
