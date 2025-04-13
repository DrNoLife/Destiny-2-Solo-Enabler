using CommunityToolkit.Mvvm.Messaging.Messages;

namespace D2SE.Application.Messages;

public class SoloPlayStatusChangedMessage(string value) : ValueChangedMessage<string>(value)
{
    public bool IsActive => Value.Equals("true");

    public static SoloPlayStatusChangedMessage Active()
        => new("true");
    public static SoloPlayStatusChangedMessage NotActive()
        => new("false");    
}
