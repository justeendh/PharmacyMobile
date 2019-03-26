using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SmsProviderServerService
{
    public partial class SendSmsService : ServiceBase
    {
        public SendSmsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServerRun.Init();
        }

        protected override void OnStop()
        {
        }
    }
}
