[![CI Build](https://github.com/IceUnicorn93/Open-VTT/actions/workflows/Build%20and%20Publish%20Open%20VTT.yml/badge.svg?branch=main)](https://github.com/IceUnicorn93/Open-VTT/actions/workflows/Build%20and%20Publish%20Open%20VTT.yml)

# What is Open VTT?

Open VTT is an Open Source Virtual Table Top.
The software I used before was not longer usable on the harware I use for running a TTRPG.
So I decided to make my own VTT.

# What are my Ideas for Open VTT?

Open VTT is supposed to run on old, weak hardware.
My initial goal were:
- RAM usage: as low as possible
- CPU usage: as low as possible
- Only use Libarys if needed, everything else needs to be self made
- If Libarys are used, only as much as needed!

All while maintaining a certain Feature-Set.
We have tested Open VTT on Raspberry Pi 4's and it worked! (You need to install mono-framework for that!)

# Instructions

1) Creating a Session/Story/Project
2) Loading a Session/Story/Project
   - 2.1) Open a file
   - 2.2) Recently Opened files
3) Configuration
   - 3.1) Automatic saving
   - 3.2) Screen Selector Settings
   - 3.3) Notes Server Settings
   - 3.4) Fog of War Settings
   - 3.5) Grid Settings
4) Working with the Scene Control
   - 4.0.1) Pinging on the Map
   - 4.0.2) Removing Fog of War
   - 4.1) Importing Maps
   - 4.2) Display Maps
   - 4.3) Working with Fog of War
     - 4.3.1) Understanding Fog of War
     - 4.3.2) Regular Fog of War
     - 4.3.3) Pre-Placed Fog of War
     - 4.3.4) Rectangle Selection
     - 4.3.5) Poligon Selection
   - 4.4) Working with Layers
          - 4.4.1) Understanding Layers
          - 4.4.2) Creating Layers
          - 4.4.3) Navigating Layers
   - 4.5) Working with Scenes
          - 4.5.1) Uderstanding Scenes
          - 4.5.2) Creating Scenes
          - 4.5.3) Navigating Scenes
5) Working with the Notes System
 -5.1) Understanding Notes Structure
 -5.2) Creating Templates
 -5.3) Creating Childs
 -5.4) Working with a remote Note Storage
  -5.4.1) Setup a remote Note Storage
  -5.4.2) Push Notes a remote Note Storage
  -5.4.3) Pull Notes from a remote Note Storage
6) Scripting
 -6.1) Understanding Scripting
 -6.2) Scripting API
 -6.3) Sample Script
7) Elgato StreamDeck
 -7.1) Understanding the StreamDeck
 -7.2) Navigating the StreamDeck
 -7.3) Static and Paging Buttons

To Clarify: If I'm talking about a Session, I refere to a Folder or the Session.xml file

# 1) Creating a Session/Story/Project

Open the Tool (Open VTT.exe) and click "Create"
A dialoge to select a folder will open, select a Folder where your Session should be saved.
In that folder a "Session.xml" File will be created.
This folder will include a copy of everything assigned to this Session.

# 2) Loading a Session/Story/Project

# 2.1) Open a file

Open the Tool (Open VTT.exe) and click "Load"
A dialoge to select a file will open, select the Session.xml file you would like to load.

# 2.2) Recently Opened files

If you created a Session with this installation of Open VTT, you will see a List of the last 8 Sessions you worked on.
Click the "Open" Button next to the Session you would like open, to load this Session.

# 3) Configuration

Click the "Configure" Button in the Start or Scene Control Window, a new Popup will open.

# 3.1) Automatic saving

The Section "Autosave" has only one option. "Auto Save (Action Based)".
If the checkbox is checked, each change in the Session will be safed automatically.

# 3.2) Screen Selector Settings

The Section "Player Screen Selector" is located in the bottom of the Window since it needs the most space.
You see a representation of your connected Displays/Screens.
In the Top right Corner of the Section you will find a Selector for "Player", "InformationDisplayPlayer" and "InformationDisplayDM".
Select "Player" and click on the Button, representing the Screen you would like to use as your Player Screeen.
A small blue Popup will appear with a sigular button saying "Okay Close". Click "Okay Close" to confirm your Selection.

"InformationDisplayPlayer" and "InformationDisplayDM" are used if you have a very specific Setup.
They are used for my personal DM-Screen Setup. You don't need to configure them!

# 3.3) Notes Server Settings

The Section for the Notes Server is located in the top right corner and has 2 Settings. IP-Adress and Port.
If your using a remote Note System, enter the IP-Adress and Port of the Note Server here.

# 3.4) Fog of War Settings

The Section for Fog of War is located on the Left side above the Screen Selector, it contains 6 Settings.


# 3.5) Grid Settings



# 4) Working with the Scene Control



# 4.0.1) Pinging on the Map



# 4.0.2) Removing Fog of War



# 4.1) Importing Maps



# 4.2) Display Maps



# 4.3) Working with Fog of War



# 4.3.1) Understanding Fog of War



# 4.3.2) Regular Fog of War



# 4.3.3) Pre-Placed Fog of War



# 4.3.4) Rectangle Selection



# 4.3.5) Poligon Selection



# 4.4) Working with Layers



# 4.4.1) Understanding Layers



# 4.4.2) Creating Layers



# 4.4.3) Navigating Layers



# 4.5) Working with Scenes



# 4.5.1) Uderstanding Scenes



# 4.5.2) Creating Scenes



# 4.5.3) Navigating Scenes



# 5) Working with the Notes System



# 5.1) Understanding Notes Structure



# 5.2) Creating Templates



# 5.3) Creating Childs



# 5.4) Working with a remote Note Storage



# 5.4.1) Setup a remote Note Storage



# 5.4.2) Push Notes a remote Note Storage



# 5.4.3) Pull Notes from a remote Note Storage



# 6) Scripting



# 6.1) Understanding Scripting



# 6.2) Scripting API



# 6.3) Sample Script



# 7) Elgato StreamDeck



# 7.1) Understanding the StreamDeck



# 7.2) Navigating the StreamDeck



# 7.3) Static and Paging Buttons

