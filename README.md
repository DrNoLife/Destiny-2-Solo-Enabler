# Destiny 2 Solo Enabler
The official repo for the program Destiny 2 Solo Enabler, or shorthand: D2SE.
Further down the page exists an FAQ, which should hopefully answer any questions you might have. However if you are still left with questions, create an Issue, and I'll respond to you!

## Notice

Since the release of lightfall, I personally haven't experienced any crashes yet.

There has been previous reports of crashes happening (particularly from 07/03/2022), where it would crash at almost random intervals. It might still happen, or it might now. Just please be aware, this has something to do with the way Bungie handles things at their end, and it's not something I can fix.


## FAQ

### What is D2SE?
D2SE is a program which creates Firewall rules and blocks out ports used by Destiny 2 (or well, rather Steam itself) for matchmaking. This enables the user of the program to load into an empty strike or an empty forge. Basically, it allows the user to not be matchmade with other players.
It works on any matchmade activity, i.e. strikes, patrol zones, story missions, etc...

For people simply curious as to how the program looks (I'm one of those people, wondering about the UI/UX before downloading a program), I've decided to include this image.

![Image depicting the UI of the Destiny 2 Solo Enabler program. Program turned off currently.](https://i.imgur.com/JYSgig0.png)
![Program turned on currently.](https://i.imgur.com/wBUynOe.png)
![Image showing the settings for the program.](https://i.imgur.com/coVHCQj.png)
![About section of the program.](https://i.imgur.com/kWID4SF.png)

Note: This is the new design. If you're interessted in the old design, download a release from before v2.

### How does one use D2SE?
When you have downloaded the program, you start up Destiny 2. Make sure you're in orbit, then you alt-tab out and enable the program. After doing this, you can search for a strike, forge, or whatever. When you want to turn it off, simply click the button in the program once again.

### Why did I create D2SE?
It's a program I decided to develop, because I got more and more annoyed with Bungie's decisions to focus on Bounties which made players compete with each other in Strikes.
I don't want to fight for kills against teammates just to complete a bounty, I want to just relax and play through a strike or two, and complete my Bounties like that. 

While there already are scripts to solve this issue, the problem with them is the lack of UX. For this program I wanted the UI itself to be simple, but still be useful at a glance. The status updates whenever certain firewall rules exist, this provides a better UX than the various PowerShell scripts out there.

### Ehh dude, \<browser name\> blocks the download of the program... is it a virus or what?
It's not a virus, that much I can guarantee. The reason for the download behaviour of the browsers, is that I didn't sign thr program. I don't have a program to sign it, nor do I have "code-signing certificate", which my Google searches told me I needed. After all, I'm just a Comp. Sci. student 3 weeks into my education.
Anyhow, you should just be able to click "Keep" on the file (atleast on Google Chrome).

### Fam, Windows also gives me a warning when I try to open it... You sure about this virus thing?
Yes, I'm quite sure! The reason for this behaviour is the same as the browser blocking the download of the program. From my expierences with opening the program on a secondary PC, the warning should only happen on the very first start up.

### Why does the program require Administrator priviledges?
Firewall handling. 
That is the reason for the prompt asking for admin priviledges. Windows has made sure a program can't change the firewall without having such rights (which is a good thing). This in turn means the program needs such rights, since that's how it does its thing!

### The program does not work! Why?
Make sure you're giving it admin priviledges. Other than that, make sure you have the .NET Framework installed (should be preinstalled on any newer Windows 10 installation, but if you are having issues, try reinstalling the latest version).
You also need to make sure you actually have the Windows firewall activated. The program relies on it, therefore it won't function without it.

### Does it work with other Firewalls than the Windows one?
No, unfortunately not. It uses namespaces which works with the Windows Firewall, and not thirdparty ones (e.g. Kaspersky). 
So make sure you're only using the Windows one.

Note: I haven't tested it myself, but one person has reported you can disable the third party Firewall while using the program, and then it should work. So if you run into problems, try disabling your third party Firewall, start the program, and when you close the program again, turn on your third party Firewall.

### Does it mean my firewall will be filled with a lot of wasteful rules after using this program a few times?
No! Don't worry. I was contemplating on just letting the firewall rules stay, and simply disabling them. However, I know that I'd find it annoying if I were to find unused firewall rules. So I made sure the programs goes in and deletes whatever rules it creates, when you (the user) decides to disable the program.

### I enabled the program, but the game won't load any strikes or anything.
When using this program, it takes longer to find matches to load into. From my testing, it takes roughly 40 seconds longer to find a strike to load into. Which means, roughly after 1 minute the game finds a strike I can load into.

### What happens when I disable the program while in a strike or some other activity?
The game will then be able to matchmake other people into your session. Which simply means, people will join you. Sometimes it takes some time (longest I've experienced was 2 minutes), other times it'll be almost instantly (shortest I've experienced was less than 5 seconds after disabling, two people joined my strike).

### How was the program developed?
Incredibly enough, Microsofts documentation for firewall handling via C# is... Lacking, if I were to phrase it kindly. But after a lot of Google Searching for information about this subject, I notices a namespace called _NetFwTypeLib_. This namespace (if I am remembering correctly), came from the NuGet package "Firewallmanager" created by cyberxander90. However, this also didn't have any documentation. But the combined strength of Google searches and randomly typing stuff to see what I could set, allowed me to find the right combination.

Feel free to checkout the Soloplay.cs file, inside there are 3 methods: One returns a bool dependent on if a FW rule already exists; one creates a firewall rule with settings given to it via its parameters; the last one goes through the entire FW rules list, and deletes whatever firewall rule it finds, if it matches the supplied firewall rule name.

### What guarantee do you have this won't ban anyone? ([source](https://www.reddit.com/r/DestinyTheGame/comments/j4fn2g/how_to_play_all_destiny_2_content_solo_dont_have/g7ilaeh?utm_source=share&utm_medium=web2x&context=3))
Good question! A complete guarantee: None.

However based on emphirical evidence, I can say that I've not experienced any problems. The very core of the program is based on a PowerShell script which has been floating around this subreddit before, and it has been used without problems.

So to answer your question, all I can say is: I've been using this program for the last week, the same has a buddy of mine. The concept of using the firewall to enable soloplay has been used by the community for a long time, and personally I've used that trick in the last 6 months. All without any problems.

### Does it work on Consoles as well?
No it does not. It's a program which makes use of an API in order to talk with the Windows firewall. Meaning, if you're not using the Windows firewall, the program will not work.
This unfortunately also means it won't work on Linux (well, since it requires .NET Framework, it won't work anyways, but yeah.)

### What's up with the Shimoneta picture?
It's an amazing series. Can only recommend it to people. A funny show which held my attention through the entire show!

## Troubleshooting

If you keep getting matched with other people you can try these solutions:

- Make absolutely sure you are using no other firewall than the default Windows one. If you have any other installed, disable those.
- Change the PowerShell execution policy. <br/>https://github.com/DrNoLife/Destiny-2-Solo-Enabler/issues/29#issuecomment-1256405397
- Check to see if there are any firewall rules which overrule the rules setup by D2SE. <br/>https://github.com/DrNoLife/Destiny-2-Solo-Enabler/issues/29#issuecomment-1236399649

Thanks to BLTplayz and MajorMalarky for the troubleshooting suggestions.

## Credits
Finding out what ports to block was not my doing. On the DestinyTheGame subreddit there already exists a PowerShell script which does what my program does. That is from where I got the port range. I would love to be able to just thank the creator of the script, however I can't seem to find out who exactly created it. Many users has posted the same script many times. So let this be a **thank you** to every single person out there posting the script!

In regards to the creation of the firewall rule, [one specific stackoverflow post](https://stackoverflow.com/a/34018032) has my eternal gratitude. This is the combined result of users [David Christiansen](https://stackoverflow.com/users/20406/david-christiansen) and [Heo Đất Hades](https://stackoverflow.com/users/2958737/heo-%c4%90%e1%ba%a5t-hades).

Massive shoutout to Github user [dheif](https://github.com/dheif). He came up with the design for the new UI, and also coded the "About" section of the program. So thank you very much!

## Ending notes
This project has been fun so far. Having to start from scratch and search up everything on your own, using whatever resources you might find, having to decide what is actually useful and what's isn't. I know I'll be using this program from now on, and I hope whoever might do the same also has a good experience with it!

Once again, if any problems are found (or just typos in this readme), feel free to create an issue on the topic. 
