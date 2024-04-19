[![CI Build](https://github.com/IceUnicorn93/Open-VTT/actions/workflows/Build%20and%20Publish%20Open%20VTT.yml/badge.svg?branch=main)](https://github.com/IceUnicorn93/Open-VTT/actions/workflows/Build%20and%20Publish%20Open%20VTT.yml)

# What is Open VTT?

Open VTT is an Open Source Virtual Table Top.
The software I used before was not longer usable on the harware I use for running a TTRPG.
So I decided to make my own VTT.

# What are my Ideas for Open VTT?

Open VTT is supposed to run perfectly on old, weak hardware.
My initial goal were:
- RAM usage: as low as possible
- CPU usage: as low as possible
- Only use Libarys if needed, everything else needs to be self made
- If Libarys are used, only as much as needed!

All while maintaining a certain Feature-Set.
We have tested Open VTT on Raspberry Pi 4's and it worked! (You need to install mono-framework for that!)

# Planned Features for the Future

> [!NOTE]
> Features may take a long time since I work on this project in my free time and in my own speed

- Rework of the Notes System
- Rework of Centroid Calculation for Pre Placed Fog of War
- Make Open VTT fully usable on Linux

# Known Problems

- [ ] Pressing like Crazy on StreamDeck Buttons causes a Crash (Specially with Set Active)
- [ ] Sometimes Cross-Platform Storys are buggy (Windows to Linux)
- [ ] Ping on Big Images causes a flicker
- [ ] Ping on Big Images is to small
- [ ] Artwork for a Note Child can't be changed once Set
- [ ] Scripting runs synchon, due to File Access Problems on Linux

# Needs Testing

- Scripting API on Linux
- StreamDeck Implementation with a StreamDeck Mini

# Getting Started

I tried my best to make the software as simple to use as possible. But let me give you a quick rundown :3 <br>
Click configure and select the Screen you want as your Player Screen, while you are there, you can test the animated map Support (You may need to install a codex for the animated maps to fully work!)
Restart the Software, Click Create and Import a map <br>
Click "Set Active" and there you go :3

# Contributing

Ok, listen I know this might sound weird but please hear me out. I would love to accept any Contribution you make at this point, but I made this Version of Open VTT for my personal Home Games.<br>
Once I finished developing the rework of the Notes System, I will put the State in a Branch called "Minimal Version" or something like that. <br>
After that Main branch becomes the Community Contribution Branch. Unfortunally due to health problems PRs may take a long time since I do this in my free time and only if I feel well enough to work on it. Thanks for understanding this

# Sepcial Thanks

A Special Thanks to the NuGet packages I used for this Project <br>
These Include (and are not limited to):
- Microsoft.CodeAnalysis.CSharp.Scripting
- StreamDeckSharp

# Wiki

I put the Instructions in the wiki [HERE](https://github.com/IceUnicorn93/Open-VTT/wiki)
Also a fea other links are there, over the time I will work on the wiki so keep an eye on that!
