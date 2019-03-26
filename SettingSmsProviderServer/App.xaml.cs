using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace SettingSmsProviderServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "ModemApp\\WirelessModem.exe");
            Thread.Sleep(5000);
            foreach (var process in Process.GetProcessesByName("WirelessModem"))
            {
                process.Kill();
            }
            base.OnStartup(e);
            
        }
    }
}
