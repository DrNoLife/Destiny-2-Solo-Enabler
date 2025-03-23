# Destiny 2 Solo Enabler
Destiny 2 Solo Enabler is a lightweight tool designed to help players load into Destiny 2 sessions without being matched with other players. By managing Windows Firewall rules, the program effectively blocks matchmaking ports, enabling solo play for strikes, forges, story missions, and more.

## Features

* **Matchmaking Block:**

    Dynamically creates and removes firewall rules to block Destiny 2 matchmaking, ensuring solo play when desired.

* **Hotkey Control:**

    Toggle solo play on or off using a dedicated hotkey.

* **Customizable Settings:**

    * Configure options such as always-on-top, hotkey enablement, and persistence of firewall rules.

    * Support for additional parameters like custom port ranges when launching the application.

* **Graceful Cleanup:**

    Automatically removes or disables firewall rules on application exit to maintain a clean system configuration.

* **Modern Architecture:**

    Built on .NET 9 using dependency injection, MVVM, and a CQRS-driven application layer to ensure clear separation of concerns and improved maintainability.

## How It Works
Destiny 2 Solo Enabler manipulates the Windows Firewall to block ports used by Destiny 2 for matchmaking. When solo play is enabled, the appropriate firewall rules are created to isolate the game session. Toggling solo play (either via the UI or a global hotkey) reverses this configuration, allowing matchmaking to resume.

## How It Looks
For people simply curious as to how the program looks (I'm one of those people, wondering about the UI/UX before downloading a program), I've decided to include this image.

![Image depicting the UI of the Destiny 2 Solo Enabler program. Program turned off currently.](https://i.imgur.com/JYSgig0.png)
![Program turned on currently.](https://i.imgur.com/wBUynOe.png)
![Image showing the settings for the program.](https://i.imgur.com/coVHCQj.png)
![About section of the program.](https://i.imgur.com/kWID4SF.png)

## FAQ

### What is D2SE?
D2SE is a program which creates Firewall rules and blocks out ports used by Destiny 2 (or well, rather Steam itself) for matchmaking. This enables the user of the program to load into an empty strike or an empty forge. Basically, it allows the user to not be matchmade with other players.
It works on any matchmade activity, i.e. strikes, patrol zones, story missions, etc...

### How does one use D2SE?

* Download and start the program.
* Boot up Destiny 2 and go into orbit.
* Enable the program.
* Start matchmaking.
* ... after some time (like 30 - 60 seconds) matchmaking will fail, and you will get an instance for yourself.

When you want to turn it off, simply click the button in the program once again.

### Why did I create D2SE?
It's a program I decided to develop, because I got more and more annoyed with Bungie's decisions to focus on Bounties which made players compete with each other in Strikes.
I don't want to fight for kills against teammates just to complete a bounty, I want to just relax and play through a strike or two, and complete my Bounties like that. 

While there already are scripts to solve this issue, the problem with them is the lack of UX. For this program I wanted the UI itself to be simple, but still be useful at a glance. The status updates whenever certain firewall rules exist, this provides a better UX than the various PowerShell scripts out there.

### What guarantee do you have this won't ban anyone? ([source](https://www.reddit.com/r/DestinyTheGame/comments/j4fn2g/how_to_play_all_destiny_2_content_solo_dont_have/g7ilaeh?utm_source=share&utm_medium=web2x&context=3))
Good question! A complete guarantee: None.

However based on emphirical evidence, I can say that I've not experienced any problems. The very core of the program is based on a PowerShell script which has been floating around this subreddit before, and it has been used without problems.

So to answer your question, all I can say is: I've been using this program for the last week, the same has a buddy of mine. The concept of using the firewall to enable soloplay has been used by the community for a long time, and personally I've used that trick in the last 6 months. All without any problems.

### Why does the program require Administrator priviledges?
Firewall handling. 
That is the reason for the prompt asking for admin priviledges. Windows has made sure a program can't change the firewall without having such rights (which is a good thing). This in turn means the program needs such rights, since that's how it does its thing!

### Does it work with other Firewalls than the Windows one?
No, unfortunately not. It uses namespaces which works with the Windows Firewall, and not thirdparty ones (e.g. Kaspersky). 
So make sure you're only using the Windows one.

Note: I haven't tested it myself, but one person has reported you can disable the third party Firewall while using the program, and then it should work. So if you run into problems, try disabling your third party Firewall, start the program, and when you close the program again, turn on your third party Firewall.

### Does it mean my firewall will be filled with a lot of wasteful rules after using this program a few times?
No! Don't worry. I was contemplating on just letting the firewall rules stay, and simply disabling them. However, I know that I'd find it annoying if I were to find unused firewall rules. So I made sure the programs goes in and deletes whatever rules it creates, when you (the user) decides to disable the program.

### What happens when I disable the program while in a strike or some other activity?
The game will then be able to matchmake other people into your session. Which simply means, people will join you. Sometimes it takes some time (longest I've experienced was 2 minutes), other times it'll be almost instantly (shortest I've experienced was less than 5 seconds after disabling, two people joined my strike).

### Does it work on Consoles as well?
No it does not. It's a program which makes use of an API in order to talk with the Windows firewall. Meaning, if you're not using the Windows firewall, the program will not work.
This unfortunately also means it won't work on Linux (well, since it requires .NET Framework, it won't work anyways, but yeah.)

### What's up with the Shimoneta picture?
It's an amazing show. Can only recommend it to people. A funny show which held my attention through the entire show!


## Troubleshooting

### Installation Warnings
Warnings about unsigned code or other security alerts are expected. These can typically be bypassed by following system prompts.

### Firewall Issues
Ensure that only the default Windows Firewall is active and that no third-party firewall software interferes with rule creation.

### Matchmaking Delays
Enabling solo play might result in longer matchmaking times. Allow additional time for matches to be found. Long story short, Bungies system needs time to actually realize that it cannot find any people for you to match with.

### Unspecified issues
If you keep getting matched with other people you can try these solutions:

- Make absolutely sure you are using no other firewall than the default Windows one. If you have any other installed, disable those.
- Change the PowerShell execution policy. <br/>https://github.com/DrNoLife/Destiny-2-Solo-Enabler/issues/29#issuecomment-1256405397
- Check to see if there are any firewall rules which overrule the rules setup by D2SE. <br/>https://github.com/DrNoLife/Destiny-2-Solo-Enabler/issues/29#issuecomment-1236399649

Thanks to BLTplayz and MajorMalarky for the troubleshooting suggestions.



## Credits & Acknowledgments

* **Original Inspiration for Port Blocking:**

The idea for blocking ports comes from a PowerShell script widely shared on the DestinyTheGame subreddit. Although the original author remains unknown due to multiple reposts, heartfelt thanks go out to everyone who contributed and refined the concept.

* **Firewall Rule Implementation:**

Special thanks to the contributors of this [Stack Overflow post](https://stackoverflow.com/a/34018032), particularly [David Christiansen](https://stackoverflow.com/users/20406/david-christiansen) and [Heo Đất Hades](https://stackoverflow.com/users/2958737/heo-%c4%90%e1%ba%a5t-hades), whose combined efforts made the creation of the firewall rule possible.

* **UI Design:**

A massive shoutout to GitHub user [dheif](https://github.com/dheif) for designing the new UI and coding the "About" section.

* **Community Support:**

I'm extremely thankful towards [BLTplayz](https://github.com/BLTplayz) for providing invaluable support in GitHub issues, as well as to [haloskinner](https://github.com/haloskinner) and [Van-Dame](https://github.com/Van-Dame) for quickly finding solutions when previous versions of the program encountered problems.
