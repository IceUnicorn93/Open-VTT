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

- Support for animated Maps
- Rework of the Notes System
- Rework of Centroid Calculation for Pre Placed Fog of War
- Make Open VTT fully usable on Linux
- Make all StreamDeck Buttons fully customizable

# Known Problems

- [ ] Pressing like Crazy on StreamDeck Buttons causes a Crash (Specially with Set Active)
- [ ] Sometimes Cross-Platform Storys are buggy (Windows to Linux)
- [ ] Ping on Big Images causes a flicker
- [ ] Ping on Big Images is to small
- [ ] Artwork for a Note Child can't be changed once Set
- [ ] Starting Open VTT will create a scripting folder from the starting location
- [ ] scripting isnt working on Linux due to wrong location on startup (Linux bug)

# Needs Testing

- Scripting API on Linux
- StreamDeck Implementation with a StreamDeck Mini

# Wiki

I put the Instructions in the wiki [HERE](https://github.com/IceUnicorn93/Open-VTT/wiki)
Also a fea other links are there, over the time I will work on the wiki so keep an eye on that!
