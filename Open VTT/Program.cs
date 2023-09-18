using Open_VTT.Forms;
using OpenVTT.Logging;
using System;
using System.Windows.Forms;

namespace Open_VTT
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _ = new Logger(Application.StartupPath);

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Start());
            }
            catch (Exception ex)
            {
                var message = $"Class: Program | Main | Exception: {ex.Message}";
                var innerEx = ex.InnerException;

                while (innerEx != null)
                {
                    message += Environment.NewLine + innerEx.Message;
                    innerEx = innerEx.InnerException;
                }

                Logger.Log(message);
            }
        }
    }
}
