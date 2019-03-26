using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace TestModemConsole
{
    public class PortMonitor
    {
        SerialPort SpGSM_Modem;
        string _portName;

        public PortMonitor(string PortName)
        {
            _portName = PortName;
            SpGSM_Modem = new SerialPort();
            SpGSM_Modem.PortName = PortName;
            SpGSM_Modem.BaudRate = 115200;
            SpGSM_Modem.DataBits = 8;
            SpGSM_Modem.StopBits = StopBits.One;
            SpGSM_Modem.Parity = Parity.None;
            SpGSM_Modem.WriteTimeout = 500;
            SpGSM_Modem.Encoding = Encoding.GetEncoding("iso-8859-1");
            SpGSM_Modem.DataReceived += SpGSM_Modem_DataReceived; ;
            SpGSM_Modem.DtrEnable = true;
            SpGSM_Modem.RtsEnable = true;
            SpGSM_Modem.Open();
        }
        
        private void SpGSM_Modem_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(string.Format("{0}: {1}", _portName, SpGSM_Modem.ReadExisting()));
        }
    }
}
