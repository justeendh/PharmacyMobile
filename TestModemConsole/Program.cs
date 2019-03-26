using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using SocketCommunicate;
using System.Threading;
using CommonLibs;

namespace TestModemConsole
{
    class Program
    {
        static AsynchronousClient client;

        static void Main(string[] args)
        {
            LoggingData.ClearLogs();
            client = new AsynchronousClient("localhost", 1450, null);
            Console.ReadLine();
            ItemData itemData = new ItemData();
            itemData.Action = "SMS_REGISTRATION_CODE";
            itemData.StateObject = "Test sms";
            while (true)
            {
                if (client != null)
                {
                    byte[] recv = client.SendData(itemData.GetBytes());
                    if(recv != null)
                    {
                        ItemData recvData = ItemData.Parse(recv);
                        Console.WriteLine(recvData.StateObject.ToString());
                    }
                }
                Console.ReadLine();
            }
        }
        
    }
}
