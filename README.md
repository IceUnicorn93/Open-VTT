# Open-VTT

Open VTT is an alternative product to dynamic dungeon editor.
The dynamic dungeon editor didn't work for many users so I started working on my own implementation of an VTT.

Open VTT has the goal of low memory use and fast loading times.

Right now the low memory use is achieved, the fast loading times could be improved by saving the state of images.
Right now the images are reloaded and the fog of war gets applyed every time a change is happening.

Open VTT also had the goal to be cross platform compatible with Linux by using the mono framework.
Using the mono framework creates interesting challenges since it needs to work on windows as well.

Open VTT is currently working on windows and Linux with a few known bugs.

1) Bringing a project from windows to Linux causes the scene images not to load for some reason
2) the ping function isn't working on Linux (needs closer inspection)
3) the save button in artwork and information isn't moving to the correct location 
4) resizing the image in a node is t saving if you don't click the save button
5) loading an image in artwork and information if an image already exists for the child causes a crash

About using Open VTT
1) go to configure and configure your screens, at least the player
2) save everything, i personally recommend to reload the software
3) click create to create a new project.
4) click import image in the to left corner and choose an image
5) click cover everything to apply fog of war for the image
6) start revealing parts of the image by clicking and dragging a rectangle on the image
6.1) alternative click the poligon button and use the poligon function
6.2) left click is a new point, right click confirms your selection
7) need a layer up or down? Like a basement or second story of a building? Use the layer up and down function
8) click new scene to create a second scene with new layers
9) click set active to activate the player and update the fog of war on player side
9.1) while in rectangle mode, right click on the map causes a ping. While in poligon mode there is no ping function

10) in information and artwork you can setup a note system and configure artworks for your notes
11) click + to create a new node or new child
11.1) node = folder, child = file
12) the notes system works on a layout system. The layout is defined by the nide and gets applyed to every child
13) add label and textboxes as you like by using the buttons in the top right corner
14) click Open viewer to open the GM Artwork display as well as the player artwork display
14.1) click on D to display the artwork of the currently selected child
15) double click on the GM Artwork display causes a ping that's also shown on player side
16) click on - to remove the node or child, removing a node opens a popup asking you to confirm the deletion of the node

17) you own an elgato stream deck? Awesome! Open VTT will recognize it and will give you quick access to switching maps, cover and revealing the the fog of war, updating the player (set active)
