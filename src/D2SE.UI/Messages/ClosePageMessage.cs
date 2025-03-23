﻿using CommunityToolkit.Mvvm.Messaging.Messages;

namespace D2SE.UI.Messages;

public class ClosePageMessage(string PageName) : ValueChangedMessage<string>(PageName)
{
    public static ClosePageMessage CloseAboutPage()
        => new("About");

    public static ClosePageMessage CloseSettingsPage()
        => new("Settings");
}
