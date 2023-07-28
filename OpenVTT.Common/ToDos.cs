namespace OpenVTT.Common
{
    internal class ToDos
    {
        // Classification: E -> Easy | M -> Medium | H -> Hard
        // Priority: 0 -> Nice to Have | 1 -> Priority | 2 -> Less Priority .. | 10 -> No priority

        //Communicated Feautes:
        //-> Rework of the Notes Section(it works but te code is ugly)
        //-> bring a copy of the 5e tools monster libary to my Notes Section
        //-> Rework the "Centroid" finding for the PrePlaced Fog of War(yes it works, but there are cases where it just breaks xD)
        //-> Make OpenVTT fully usable on Linux
        //-> Make Version with Video Support (Using axWindowsMediaPlayer)

        //Maybe:
        //-> An Initiative Tracker
        //-> Display Memory Usage
        //-> Scene Templates for Scenes the Players visit frequently


        //--------------------------------------------------------------------
        // Active ToDos
        //--------------------------------------------------------------------

        // Working on Linux with StreamDeck!

        // Bugs:
        /*
         * Pressing Buttons like Crazy on the StreamDeck can cause a crash
         *  Specially: {Random Buttons} layer switching, Cover all
         */

        // Needs Testing:
        /*
         * Creating Storys so they are compatible for every Platform
         * Ping on big Images
         * Importing an ArtworkImage if ArtworkImage is already set
         */

        // Initiative Tracker
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
        /* Idea:
         * New Window, special for notes notes are not tied to a scene
         */

        //Idea: {H0} "Initiave"-Tracker with Artwork Display

        //Idea: {M0} Think About Scene-Templates (Saved in Editor Directory)
        /* Create Scenes as Templates. Templaes will be safed in the Editor Directory
         */
    }
}
