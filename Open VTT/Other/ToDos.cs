namespace Open_VTT.Other
{
    class ToDos
    {
        // Classification: E -> Easy | M -> Medium | H -> Hard
        // Priority: 0 -> Nice to Have | 1 -> Priority | 2 -> Less Priority .. | 10 -> No priority

        //--------------------------------------------------------------------
        // Active ToDos
        //--------------------------------------------------------------------

        // Main Map Control fully working on Linux!

        // Bugs:
        /*
         * nothing!
         */

        // Needs Testing:
        /*
         * - Library (Add, Remove)
         * - Custom Layout
         * - Artwork Display with Ping
         * 
         */

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

        //Idea: {H0} StreamDeck Support
        /* [Layer Up] [Reveal All] [Scene 1] [Scene 2] [Scene 3]
         * [Layer Dn] [Cover All ] [Scene 4] [Scene 5] [Scene 6]
         * [Nothing ] [Set Active] [Prev Pg] [Curr Pg] [Next Pg]
         */

        //Idea: {M0} Pre-Place Fog of War Sections and make it toggleble

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
