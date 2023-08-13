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

# Needs Testing

- Scripting API on Linux
- StreamDeck Implementation with a StreamDeck Mini

# Instructions

> [!NOTE]
> The Tool is pretty self explaining. Don't read all of this. Only read the parts you don't understand.
> The Instructions are ment to be read with Open VTT open.
> Honestly, <you don't want to read all of this!

1) [Creating a Session/Story/Project](https://github.com/IceUnicorn93/Open-VTT/tree/main#1-creating-a-sessionstoryproject)
2) [Loading a Session/Story/Project](https://github.com/IceUnicorn93/Open-VTT/tree/main#2-loading-a-sessionstoryproject)
   - 2.1) [Open a file](https://github.com/IceUnicorn93/Open-VTT/tree/main#21-open-a-file)
   - 2.2) [Recently Opened files](https://github.com/IceUnicorn93/Open-VTT/tree/main#22-recently-opened-files)
3) [Configuration](https://github.com/IceUnicorn93/Open-VTT/tree/main#3-configuration)
   - 3.1) [Automatic saving](https://github.com/IceUnicorn93/Open-VTT/tree/main#31-automatic-saving)
   - 3.2) [Screen Selector Settings](https://github.com/IceUnicorn93/Open-VTT/tree/main#32-screen-selector-settings)
   - 3.3) [Notes Server Settings](https://github.com/IceUnicorn93/Open-VTT/tree/main#33-notes-server-settings)
   - 3.4) [Fog of War Settings](https://github.com/IceUnicorn93/Open-VTT/tree/main#34-fog-of-war-settings)
   - 3.5) [Grid Settings](https://github.com/IceUnicorn93/Open-VTT/tree/main#35-grid-settings)
4) [Working with the Scene Control](https://github.com/IceUnicorn93/Open-VTT/tree/main#4-working-with-the-scene-control)
   - 4.0.1) [Pinging on the Map](https://github.com/IceUnicorn93/Open-VTT/tree/main#401-pinging-on-the-map)
   - 4.1) [Importing Maps](https://github.com/IceUnicorn93/Open-VTT/tree/main#41-importing-maps)
   - 4.2) [Display Maps](https://github.com/IceUnicorn93/Open-VTT/tree/main#42-display-maps)
   - 4.3) [Working with Fog of War](https://github.com/IceUnicorn93/Open-VTT/tree/main#43-working-with-fog-of-war)
     - 4.3.1) [Understanding Fog of War](https://github.com/IceUnicorn93/Open-VTT/tree/main#431-understanding-fog-of-war)
     - 4.3.2) [Regular Fog of War](https://github.com/IceUnicorn93/Open-VTT/tree/main#432-regular-fog-of-war)
     - 4.3.3) [Pre-Placed Fog of War](https://github.com/IceUnicorn93/Open-VTT/tree/main#433-pre-placed-fog-of-war)
     - 4.3.4) [Rectangle Selection](https://github.com/IceUnicorn93/Open-VTT/tree/main#434-rectangle-selection)
     - 4.3.5) [Poligon Selection](https://github.com/IceUnicorn93/Open-VTT/tree/main#435-poligon-selection)
   - 4.4) [Working with Layers](https://github.com/IceUnicorn93/Open-VTT/tree/main#44-working-with-layers)
     - 4.4.1) [Understanding Layers](https://github.com/IceUnicorn93/Open-VTT/tree/main#441-understanding-layers)
     - 4.4.2) [Creating Layers](https://github.com/IceUnicorn93/Open-VTT/tree/main#442-creating-layers)
     - 4.4.3) [Navigating Layers](https://github.com/IceUnicorn93/Open-VTT/tree/main#443-navigating-layers)
   - 4.5) [Working with Scenes](https://github.com/IceUnicorn93/Open-VTT/tree/main#45-working-with-scenes)
     - 4.5.1) [Uderstanding Scenes](https://github.com/IceUnicorn93/Open-VTT/tree/main#451-uderstanding-scenes)
     - 4.5.2) [Creating Scenes](https://github.com/IceUnicorn93/Open-VTT/tree/main#452-creating-scenes)
     - 4.5.3) [Navigating Scenes](https://github.com/IceUnicorn93/Open-VTT/tree/main#453-navigating-scenes)
5) [Working with the Notes System](https://github.com/IceUnicorn93/Open-VTT/tree/main#5-working-with-the-notes-system)
   - 5.1) [Understanding Notes Structure](https://github.com/IceUnicorn93/Open-VTT/tree/main#51-understanding-notes-structure)
   - 5.2) [Creating Templates](https://github.com/IceUnicorn93/Open-VTT/tree/main#52-creating-templates)
   - 5.3) [Creating Childs](https://github.com/IceUnicorn93/Open-VTT/tree/main#53-creating-childs)
   - 5.4) [Working with a remote Note Storage](https://github.com/IceUnicorn93/Open-VTT/tree/main#54-working-with-a-remote-note-storage)
     - 5.4.1) [Setup a remote Note Storage](https://github.com/IceUnicorn93/Open-VTT/tree/main#541-setup-a-remote-note-storage)
     - 5.4.2) [Push Notes a remote Note Storage](https://github.com/IceUnicorn93/Open-VTT/tree/main#542-push-notes-a-remote-note-storage)
     - 5.4.3) [Pull Notes from a remote Note Storage](https://github.com/IceUnicorn93/Open-VTT/tree/main#543-pull-notes-from-a-remote-note-storage)
6) [Scripting](https://github.com/IceUnicorn93/Open-VTT/tree/main#6-scripting)
   - 6.1) [Understanding Scripting](https://github.com/IceUnicorn93/Open-VTT/tree/main#61-understanding-scripting)
   - 6.2) [Scripting API](https://github.com/IceUnicorn93/Open-VTT/tree/main#62-scripting-api)
   - 6.3) [Sample Script](https://github.com/IceUnicorn93/Open-VTT/tree/main#63-sample-script)
7) [Elgato StreamDeck](https://github.com/IceUnicorn93/Open-VTT/tree/main#7-elgato-streamdeck)
   - 7.1) [Understanding the StreamDeck](https://github.com/IceUnicorn93/Open-VTT/tree/main#71-understanding-the-streamdeck)
   - 7.2) [Navigating the StreamDeck](https://github.com/IceUnicorn93/Open-VTT/tree/main#72-navigating-the-streamdeck)
   - 7.3) [Static and Paging Buttons](https://github.com/IceUnicorn93/Open-VTT/tree/main#73-static-and-paging-buttons)

To Clarify: If I'm talking about a Session, I refere to a Folder or the Session.xml file

# 1) Creating a Session/Story/Project

Open the Tool (Open VTT.exe) and click "Create"
A dialoge to select a folder will open, select a Folder where your Session should be saved.
In that folder a "Session.xml" File will be created.
This folder will include a copy of everything assigned to this Session.

# 2) Loading a Session/Story/Project

## 2.1) Open a file

Open the Tool (Open VTT.exe) and click "Load"
A dialoge to select a file will open, select the Session.xml file you would like to load.

## 2.2) Recently Opened files

If you created a Session with this installation of Open VTT, you will see a List of the last 8 Sessions you worked on.
Click the "Open" Button next to the Session you would like open, to load this Session.

# 3) Configuration

Click the "Configure" Button in the Start or Scene Control Window, a new Popup will open.
Any changes to the Settings will be safed automatically!

## 3.1) Automatic saving

The Section "Autosave" has only one option. "Auto Save (Action Based)".
If the checkbox is checked, each change in the Session will be safed automatically.

## 3.2) Screen Selector Settings

The Section "Player Screen Selector" is located in the bottom of the Window since it needs the most space.
You see a representation of your connected Displays/Screens.
In the Top right Corner of the Section you will find a Selector for "Player", "InformationDisplayPlayer" and "InformationDisplayDM".
Select "Player" and click on the Button, representing the Screen you would like to use as your Player Screeen.
A small blue Popup will appear with a sigular button saying "Okay Close". Click "Okay Close" to confirm your Selection.

"InformationDisplayPlayer" and "InformationDisplayDM" are used if you have a very specific Setup.
They are used for my personal DM-Screen Setup. You don't need to configure them!

## 3.3) Notes Server Settings

The Section for the Notes Server is located in the top right corner and has 2 Settings. IP-Adress and Port.
If your using a remote Note System, enter the IP-Adress and Port of the Note Server here.

## 3.4) Fog of War Settings

The Section for Fog of War is located on the Left side above the Screen Selector, it contains 6 Settings.
"Display Changes instantly", check this checkbox to display any changes to the Fog of War to the playerys directly
"Display Grid", check this checkbox if you want to use a 1" Grid on the player side
"DM Color", click on the square right next to the Text to open a Popup where you can Set the Color for the DM Fog of War
"Player Color", click on the square right next to the Text to open a Popup where you can Set the Color for the Player Fog of War
"Grid Color", click on the square right next to the Text to open a Popup where you can Set the Grid Color
"Text Color", click on the square right next to the Text to open a Popup where you can Set the Text Color for the Pre-Placed Fog of War

## 3.5) Grid Settings

The Section for the Grid Settings are in the top left corner and contains 2 Settings.
"Player Window Size", enter the Size of your Player Screen here, if it's a 43" Screen, enter 43
"Display Grid for DM", check this Box if you want to see the Grid on the DM view as well
> [!IMPORTANT]
> Enter the Player Window Size if you plan to use a Grid!

# 4) Working with the Scene Control

The Scene Control is the heart of the VTT, with this window you will manipulte the Fog of War, Select the Image to display and so on!
This window only opens if you Loaded or Created a Story.

## 4.0.1) Pinging on the Map

If a map is Importet to this layer, make sure to select "Rectangle" and "Imidiate" in the top part of Scene Control.
Right click on a part of the map you want to set a ping to. A ping Marker will appear for a few seconds.
This Marker will be displayed on the Player Window as well!

> [!IMPORTANT]
> Please don't spam right click, the process is design to handle on ping after another. setting a new ping while a ping is still active causes weird effects!


## 4.1) Importing Maps

Click on "Import Map". This will open a dialoge to choose an image or a video file (animated map).
Select the file you want and it will be added to the Session and will become the map for the active layer.

## 4.2) Display Maps

> [!IMPORTANT]
> Please make sure to first set the Player Screen in the Config Menu!

Click on "Set Active" in order to display the Map to the players.

## 4.3) Working with Fog of War

Fog of War is what a VTT makes a VTT and not simply an Image Display.

### 4.3.1) Understanding Fog of War

Fog of War hides certain areas of a map so players don't see everything directly.
Each Layer can have multiple Fog of War "entities".

### 4.3.2) Regular Fog of War

Regular Fog of War will be removed on loading a layer. To use regular Fog of War select "Imidiate" in the Selection.
For Example your Layer displays a Building.
You don't want to show the inside of the building, but the outside is okay.
So you can use regular Fog of War to clear the Fog for the Outside of the Building.

### 4.3.3) Pre-Placed Fog of War

Pre Placed Fog of War will not beremoved if a Layer gets loaded. To use Pre Placed Fog of War select "Pre-Place" in the Seleciton.
To continue the example from before, the Building has multiple rooms.
You can use Pre Placed Fog of War to map out the Building.
Once you selected an Area, a popup will open asking you for a Name of this selection. Give it a good, unique name since this Name will be used to identify it later on!

### 4.3.4) Rectangle Selection

Rectangle Selection for the Fog of War is excatly what it says. It's a rectangluar selection of Fog of War that gets removed once you finish the Selection.
To use the Rectable Selection use the "Rectangle" Button in the Top.
Left click and hold the Mouse Button in the Map and drag it as big as you need.
Release the Button to confirm your Selection. The Fog of War will now be removed for this Selection.

### 4.3.5) Poligon Selection

Poligon Selection for the Fog of War is everything thats not a rctangle, a hexagon, a pentagon, every shape you can imagine!
To use the Poligon Selection use "Poligon" in the Top.
Use a Left click to start your poligon selection. Each new left click sets a new Poligon-Point.
Once you selected the shape you want, use Right click to confirm your selection. 

## 4.4) Working with Layers



### 4.4.1) Understanding Layers



### 4.4.2) Creating Layers



### 4.4.3) Navigating Layers



## 4.5) Working with Scenes



### 4.5.1) Uderstanding Scenes



### 4.5.2) Creating Scenes



### 4.5.3) Navigating Scenes



# 5) Working with the Notes System



## 5.1) Understanding Notes Structure



## 5.2) Creating Templates



## 5.3) Creating Childs



## 5.4) Working with a remote Note Storage



### 5.4.1) Setup a remote Note Storage



### 5.4.2) Push Notes a remote Note Storage



### 5.4.3) Pull Notes from a remote Note Storage



# 6) Scripting



## 6.1) Understanding Scripting



## 6.2) Scripting API



## 6.3) Sample Script

```
Small Easy Sample Script!
```

# 7) Elgato StreamDeck



## 7.1) Understanding the StreamDeck



## 7.2) Navigating the StreamDeck



## 7.3) Static and Paging Buttons

