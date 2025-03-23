namespace D2SE.Domain.Interfaces.Application;

public interface IHotkeyNotification
{
    Task OnHotkeyPressed(string hotkeyId);
}