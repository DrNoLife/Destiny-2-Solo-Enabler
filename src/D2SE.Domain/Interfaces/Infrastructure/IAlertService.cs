namespace D2SE.Domain.Interfaces.Infrastructure;

public interface IAlertService
{
    public void ShowAlert(string message, bool forceAlert = false);
    public void ShowAlert(string title, string message, bool forceAlert = false);
}
