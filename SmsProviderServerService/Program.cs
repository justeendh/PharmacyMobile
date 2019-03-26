using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SmsProviderServerService
{
    static class Program
    {

        static void Main()
        {
            ServerRun.Init();
            Application.Run();

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SendSmsService()
            //};
            //ServiceBase.Run(ServicesToRun);

        }


    }
}
