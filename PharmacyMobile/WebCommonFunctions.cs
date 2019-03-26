using SocketCommunicate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PharmacyMobile
{
    public class WebCommonFunctions
    {
        public static bool RequestSendSms(string number, string content)
        {
            AsynchronousClient client = null;
            try
            {
                string SmsServer = ConfigurationManager.AppSettings["SmsServer"];
                client = new AsynchronousClient(SmsServer, 1450, null);
                if(client != null && client.IsConnected())
                {
                    Dictionary<string, string> dictDataSend = new Dictionary<string, string>();
                    dictDataSend.Add("PHONE_NUMBER", number);
                    dictDataSend.Add("CONTENT_SMS", content);
                    ItemData itemData = new ItemData();
                    itemData.Action = "SMS_REGISTRATION_CODE";
                    itemData.StateObject = dictDataSend;
                    byte[] recv = client.SendData(itemData.GetBytes());
                    if (recv != null)
                    {
                        ItemData recvData = ItemData.Parse(recv);
                        if (client != null && client.IsConnected()) client.Disconnect();
                        return (bool)recvData.StateObject;
                    }
                }
                if (client != null && client.IsConnected()) client.Disconnect();
                return false;
            }
            catch
            {
                if (client != null && client.IsConnected()) client.Disconnect();
                return false;
            }
        }
    }
}