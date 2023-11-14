﻿namespace OpenVTT.Common
{
    internal class ToDos
    {
        // Classification: E -> Easy | M -> Medium | H -> Hard
        // Priority: 0 -> Nice to Have | 1 -> Priority | 2 -> Less Priority .. | 10 -> No priority

        //Communicated Feautes:
        //-> Rework of the Notes Section(it works but te code is ugly)
        //-> bring a copy of the 5e tools monster libary to my Notes Section
        //-> Rework the "Centroid" finding for the PrePlaced Fog of War(yes it works, but there are cases where it just breaks xD)
        //-> Make OpenVTT fully usable on Linux (Sometimes Problems with Updating FOW, Scripting has Problems with DLL references)
        //-> Make Version with Video Support (Using axWindowsMediaPlayer)

        //Maybe?:
        //-> Display Memory Usage


        //--------------------------------------------------------------------
        // Active ToDos
        //--------------------------------------------------------------------

        //Actively under work:
        /*
         * -> Animated Map Support
         *      For what ever reason they want it
         * -> [x] Add Designer for Notes and new Scipts (OpenVTT.UiDesigner)
         *      Custom designs for the Notes
         * -> Finetuning for Grid-Values
         *      Sometimes Grids can be slightly rectangluar (specially on ultra wide Monitors)
         */

        // Bugs:
        /*
         * Pressing Buttons like Crazy on the StreamDeck can cause a crash
         *  Specially: {Random Buttons} layer switching, Cover all
         * 
         * Grid will only be displayed if some kind of Fog of War is in the Layer
         *  Should be changed to always display
         * 
         * Grid works only in 1" squares if the base Image has the same resolution as the Display
         */

        // Needs Testing:
        /*
         * Creating Storys so they are compatible for every Platform
         * Ping on big Images
         * Importing an ArtworkImage if ArtworkImage is already set
         */

        // Memory Activity
        // Digital Monster Library

        //Idea: {H0} Digital Monster Library (Stats, Attacks, Artwork)
        //--------> Include Editor for Layout

        //--------------------------------------------------------------------
        // Planned ToDos
        //--------------------------------------------------------------------

        //Idea: {M0} Memory Activity
        /* Show Currecntly used memory of this application somewhere in the application
         * long memory = GC.GetTotalMemory(true);

         *  using System.Diagnostics;
            Process currentProc = Process.GetCurrentProcess();
            long memoryUsed = currentProc.PrivateMemorySize64;
         */

        //--------------------------------------------------------------------
        // Future ToDos
        //--------------------------------------------------------------------

        //Idea: {H1} Work on automated tests

        //Idea: {M0} Support Session Notes (Optionally)
        /* New Window, special for notes notes are not tied to a scene
         */

        //Idea: {M0} Think About Scene-Templates (Saved in Editor Directory)
        /* Could be realized with a Script Module.
         * Create a new Story, fill with "Default Stuff" press a Button to save it as Templte.json
         * Each Time a new Story is created, in that TAB press the Button to load the Template.
         * In the easiest form this would just override the active Session, in an more advanced scenraio missing Stuff would be added
         * The perfect Solution would be to see all Scenes and layers and Choose what to add
         */
    }
}
