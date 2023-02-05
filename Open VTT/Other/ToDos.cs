﻿namespace Open_VTT.Other
{
    class ToDos
    {
        // Classification: E -> Easy | M -> Medium | H -> Hard
        // Priority: 0 -> Nice to Have | 1 -> Priority | 2 -> Less Priority .. | 10 -> No priority

        //--------------------------------------------------------------------
        // Active ToDos
        //--------------------------------------------------------------------

        // Working on Linux with StreamDeck!

        // Bugs:
        /*
         * Pressing Buttons like Crazy on the StreamDeck can cause a crash
         * Specially, {Random Buttons} layer switching, Cover all
         */

        // Needs Testing:
        /*
         * Nothing!
         */

        // Currently Implementing: Nothing

        //Idea: {H0} Digital Monster Library (Stats, Attacks, Artwork)
        //--------> Include Editor for Layout
        //Idea: {M0} Pre-Place Fog of War Sections and make it toggleble

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
        /* Idea:
         * New Window, special for notes notes are not tied to a scene
         */

        //Idea: {H0} "Initiave"-Tracker with Artwork Display

        //Idea: {M0} Think About Scene-Templates (Saved in Editor Directory)
        /* Create Scenes as Templates. Templaes will be safed in the Editor Directory
         */
    }
}
