using CommonLibs;
using ModemModule;
using SocketCommunicate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmsProviderServerService
{
    public static class ServerRun
    {

        public static AsynchronousSocketListener server;
        public static GSMModemDevice modem;
        public static bool DeviceRemove = false;

        public static void RunServer()
        {
            server = new AsynchronousSocketListener(1450, (itemData, serverSms, clientSocket) => {
                switch (itemData.Action)
                {
                    case "SMS_REGISTRATION_CODE":
                        if (modem != null && modem.IsOpened())
                        {
                            Dictionary<string, string> dictDataRecv = itemData.StateObject as Dictionary<string, string>;
                            if (dictDataRecv != null)
                            {
                                string phoneNumber = dictDataRecv["PHONE_NUMBER"];
                                string contentSms = dictDataRecv["CONTENT_SMS"];
                                bool rs = modem.SendLongSMS(phoneNumber, contentSms);
                                Console.WriteLine(string.Format("{0}: Send SMS \"{1}\" to {2} => {3}", DateTime.Now, contentSms, phoneNumber, rs ? "Successfully" : "Failed"));
                                serverSms.SendData(clientSocket, (new ItemData() { Action = "SMS_REGISTRATION_CODE", StateObject = rs }).GetBytes());
                            }
                            serverSms.SendData(clientSocket, (new ItemData() { Action = "SMS_REGISTRATION_CODE", StateObject = false }).GetBytes());
                        }
                        break;
                    default:
                        serverSms.SendData(clientSocket, (new ItemData() { Action = itemData.Action, StateObject = false }).GetBytes());
                        break;
                }
                Dictionary<string, object> dictData = itemData.StateObject as Dictionary<string, object>;
                serverSms.SendData(clientSocket, itemData.GetBytes());
            });
            server.Start();
        }

        public static void Init()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"o========================================================================o");
            Console.WriteLine(@"|    _____ __  __  _____    _____ ______ _______      ________ _____     |");
            Console.WriteLine(@"|   / ____|  \/  |/ ____|  / ____|  ____|  __ \ \    / /  ____|  __ \    |");
            Console.WriteLine(@"|  | (___ | \  / | (___   | (___ | |__  | |__) \ \  / /| |__  | |__) |   |");
            Console.WriteLine(@"|   \___ \| |\/| |\___ \   \___ \|  __| |  _  / \ \/ / |  __| |  _  /    |");
            Console.WriteLine(@"|   ____) | |  | |____) |  ____) | |____| | \ \  \  /  | |____| | \ \    |");
            Console.WriteLine(@"|  |_____/|_|  |_|_____/  |_____/|______|_|  \_\  \/   |______|_|  \_\   |");
            Console.WriteLine(@"|                 _   _ _                      _                         |");
            Console.WriteLine(@"|                 | | | (_)                    (_)                       |");
            Console.WriteLine(@"|                 | | | |_ _ __   __ ___      ___ ___  ___               |");
            Console.WriteLine(@"|                 | | | | | '_ \ / _` \ \ /\ / / / __|/ _ \              |");
            Console.WriteLine(@"|                 \ \_/ / | | | | (_| |\ V  V /| \__ \  __/              |");
            Console.WriteLine(@"|                  \___/|_|_| |_|\__,_| \_/\_/ |_|___/\___|              |");
            Console.WriteLine(@"|                                                                        |");
            Console.WriteLine(@"o========================================================================o");
            Console.ResetColor();
            Console.WriteLine(string.Format("{0}: Starting modem application to init drivers", DateTime.Now));
            LoggingData.ClearLogs();
            try
            {
                Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "ModemApp\\WirelessModem.exe");
                Thread.Sleep(5000);
                foreach (var process in Process.GetProcessesByName("WirelessModem"))
                {
                    process.Kill();
                }
                Thread.Sleep(3000);
                Console.WriteLine(string.Format("{0}: Loading GSM modem control ", DateTime.Now));
                do
                {
                    Dictionary<string, object> dictData = CommonFunctions.GetSerializedObject(System.AppDomain.CurrentDomain.BaseDirectory + "GsmSettings.dat") as Dictionary<string, object>;
                    string PortName = dictData["PORT_NAME"] as string;
                    int BaudRate = (int)dictData["COM_BAUDRATE"];
                    int DataBit = (int)dictData["COM_DATA_BIT"];
                    StopBits stopBits = (StopBits)dictData["COM_STOP_BIT"];
                    Parity parity = (Parity)dictData["COM_PARITY"];
                    modem = new GSMModemDevice(PortName, BaudRate, 8, stopBits, parity);
                    modem.OpenPort();
                    if (modem != null && modem.IsOpened())
                    {
                        modem.InitModemConfig();
                        modem.setModemSIMReady();
                        Console.WriteLine(string.Format("{0}: GSM Modem ready on {1}", DateTime.Now, PortName));
                        break;
                    }
                    Console.WriteLine(string.Format("{0}: GSM Modem open port failed. Check your device is connected. Retry openmodem ", DateTime.Now));
                    Thread.Sleep(5000);
                } while (modem == null || !modem.IsOpened());

                SerialPortService.PortsChanged += SerialPortService_PortsChanged;

                Thread threadServerSms = new Thread(RunServer);
                threadServerSms.IsBackground = true;
                threadServerSms.Start();

                Console.WriteLine(string.Format("{0}: System is ready ...", DateTime.Now));
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                Console.WriteLine("{0:dd-MM-yyyy HH:mm:ss}: {1} \r\nStackTrace: {2}", DateTime.Now, ex.Message, ex.StackTrace);
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "SmsProviderServerService.exe");
                Environment.Exit(0);
            }
        }

        private static void SerialPortService_PortsChanged(object sender, PortsChangedArgs e)
        {
            if(e.EventType == EventType.Removal && modem != null && !e.SerialPorts.Contains(modem.PortName))
            {
                DeviceRemove = true;
                Console.WriteLine(string.Format("{0}: Device removed. Please connect device ...", DateTime.Now));
                return;
            }

            if(e.EventType == EventType.Insertion && DeviceRemove)
            {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "SmsProviderServerService.exe");
                Environment.Exit(0);
            }
        }
    }
}
