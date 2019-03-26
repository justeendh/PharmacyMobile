using GSM_Modem.Forms;
using ModemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GSM_Modem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestSendSMS());
        }
    }
}
