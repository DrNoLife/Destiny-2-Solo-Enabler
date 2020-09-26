# Destiny 2 Solo Enabler
The official repo for the program Destiny 2 Solo Enabler, or shorthand: D2SE.
Further down the page exists an FAQ, which should hopefully answer any questions you might have. However if you are still left with questions, create an Issue, and I'll respond to you!

## FAQ

### What is D2SE?
D2SE is a program which creates Firewall rules and blocks out ports used by Destiny 2 (or well, rather Steam itself) for matchmaking. This enables the user of the program, to load into an empy strike, or an empty forge. Basically, it allows the user to not be matches up with other players.

For people simply curious as to how the program looks (I'm one of those people, wondering about the UI/UX before downloading a program), I've decided to include this image.

![Image depicting the UI of the Destiny 2 Solo Enabler program.](https://i.imgur.com/tsZdZQO.png)


### Why did I create D2SE?
It's a program I decided to develop, because I got more and more annoyed with Bungies decisions to focus on Bounties which made players compete with eachother in Strikes.
I don't want to fight for kills against teammates just to complete a bounty, I want to just relax and play through a strike or two, and complete my Bounties like that. 

While there already exists scripts to solve this issue, the problem with them is the lack of UX. For this program I wanted the UI itself to be simple, but still be useful at a glance. The status updates whenever certain firewall rules exist, this provides a better UX than the various PowerShell scripts out there.

### Ehh dude, \<browser name\> blocks the download of the program... is it a virus or what?
It's not a virus, that I can guarantee. The reason for the download behaviour of the browsers, is that I didn't sign thr program. I don't have a program to sign it, nor do I have "code-signing certificate", which my Google searches told me I needed. After all, I'm just a Comp. Sci. student 3 weeks into my education.
Anyhow, you should just be able to click "Keep" on the file (atleast of Google Chrome).

### Fam, Windows also gives me a warning when I try to open it... You sure about this virus thing?
Yes, I'm quite sure! The reason for this behaviour is the same as browsers blocking the download of the program. From my expierences with opening thr program on a secondary PC, the warning should only happen on the very first start up.

### Why does the program require Administrator priviledges?
Firewall handling. 
That is the reason for the prompt asking for admin priviledges. Windows has made sure a program can't change the firewall without having such rights (which is a good thing). This in turn means, the program needs such rights, since that's how it does its thing!

### Does it mean my firewall will be filled with a lot of wasteful rules after using this program a few times?
No! Don't worry. I was contemplating on just letting the firewall rules stay, and simply disabling them. However, I know that I'd find it annoying if I were to find unused firewall rules. So I made sure thr programs goes in and deletes whatever rules it creates, when you (the user) decides to disable the program.

### How was the program developed?
Incredibly enough, Microsofts documentation for firewall handling via C# is... Lacking, if I were to phrase it kindly. But after a lot of Google Searching for information about this subject, I notices a namespace called _NetFwTypeLib_. This namespace (if I am remembering correctly), came from the NuGet package "Firewallmanager" created by cyberxander90. However, this also didn't have any documentation. But the combined strength of Google searches and randomly typing stuff to see what I could set, allowed me to find the right combination.

Feel free to checkout the Soloplay.cs file, inside there are 3 methods: One returns a bool dependent on if a FW rule already exists; one creates a firewall rule with settings given to it via its parameters; the last one goes through the entire FW rules list, and deletes whatever firewall rule it finds, if it matches the supplied firewall rule name.

## Credits
Finding out what ports to block was not my doing. On the DestinyTheGame subreddit already exists a PowerShell script which does what my program does. That is from where I got the port range. I would love to be able to just thank the creator of the script, however I can't seem to find out who exactly created it. Many users has posted the same script many times. So let this be a **thank you** to every single person out there posting the script!

In regards to the creation of the firewall rule, [one specific stackoverflow post](https://stackoverflow.com/a/34018032) has my eternal gratitude. This is the combined result of users [David Christiansen](https://stackoverflow.com/users/20406/david-christiansen) and [Heo Đất Hades](https://stackoverflow.com/users/2958737/heo-%c4%90%e1%ba%a5t-hades).

## Ending notes
This project has been fun so far. Having to start from scratch and search up everything on your own, using whatever resources you might find, having to decide what is actually useful and what's not. I know I'll be using this program from now on, and I hope whoever might do the same also has a good experience with it!

Once again, if any problems are found (or just typos in this readme), feel free to create an issue on the topic. 
