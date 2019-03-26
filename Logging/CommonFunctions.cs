using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CommonLibs
{
    public class CommonFunctions
    {
        public static string BoDau(string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        }

        //Compress ObjectData
        public static string ToStringBase64(object object_data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, object_data);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static object LoadFromString(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }
        
        public static DateTime GetStartDate(int month, int year)
        {
            ConfigData config = new ConfigData();
            int Date_Start = config.getConfigValue(CONFIGKEY.DATE_START_ATT) == null ? 1 : (int)config.getConfigValue(CONFIGKEY.DATE_START_ATT);
            DateTime startDate = new DateTime(year, month, Date_Start);

            return startDate;
        }
        
        public static object GetSerializedObject(string filename)
        {
            IFormatter iformatter = new BinaryFormatter();
            if (File.Exists(filename))
            {
                Stream inStream = new FileStream(
                    filename,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
                object obj = (object) iformatter.Deserialize(inStream);
                inStream.Close();
                return obj;
            }
            return default(object);
        }

        public static void SaveSerializedObject(object obj, string filename)
        {
            IFormatter iformatter = new BinaryFormatter();
            Stream outStream = new FileStream(
                filename,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);
            iformatter.Serialize(outStream, obj);
            outStream.Close();
        }

        public static T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        public static byte[] Serialize<T>(T param)
        {
            byte[] encMsg = null;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter br = new BinaryFormatter();
                br.Serialize(ms, param);
                encMsg = ms.ToArray();
            }
            return encMsg;
        }

        public static List<string> GetListSerialPort()
        {
            List<string> PortsResult = new List<string>();
            string[] lstPort = SerialPort.GetPortNames();
            foreach (string itemPort in lstPort)
            {
                PortsResult.Add(itemPort);
            }
            return PortsResult;
        }

        public static List<string> GetListModem()
        {
            List<string> ModemsResult = new List<string>();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem"))
            {
                if (searcher.Get() != null && searcher.Get().Count > 0)
                {
                    foreach (ManagementObject item in searcher.Get())
                    {
                        ModemsResult.Add(item["AttachedTo"].ToString());
                    }
                }
            }
            return ModemsResult;
        }

        public static Dictionary<string, SerialPortItem> GetDictSerialPort(IEnumerable<string> except = null)
        {
            Dictionary<string, SerialPortItem> PortsResult = new Dictionary<string, SerialPortItem>();
            string[] lstPort = SerialPort.GetPortNames();
            foreach (string itemPort in lstPort)
            {
                if(except != null && except.Count() > 0 && (except.Contains(itemPort))) continue;

                using (var searcher = new ManagementObjectSearcher("root\\CIMV2", string.Format("SELECT * FROM Win32_PnPEntity WHERE Caption like '%({0})%'", itemPort)))
                {
                    if(searcher.Get() != null && searcher.Get().Count > 0)
                    {
                        ManagementObject[] queryObjs = new ManagementObject[searcher.Get().Count];
                        searcher.Get().CopyTo(queryObjs, 0);
                        PortsResult.Add(itemPort, new SerialPortItem() { Description = queryObjs[0]["Caption"].ToString(), PortName = itemPort });
                    }
                }
            }
            return PortsResult;
        }

        public static IEnumerable<string> SmartSplit(string input, int maxLength)
        {
            int i = 0;
            while (i + maxLength < input.Length)
            {
                int index = input.LastIndexOf(' ', i + maxLength);
                if (index <= 0) //if word length > maxLength.
                {
                    index = maxLength;
                }
                yield return input.Substring(i, index - i);

                i = index + 1;
            }

            yield return input.Substring(i);
        }

        public static bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;
            return input.Any(c => c > MaxAnsiCode);
        }

        public static string CreateString(int stringLength)
        {
            Random rd = new Random();
            const string allowedChars = "0123456789";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        static public object GetFeedData(string urlRss)
        {            
            string[] FeedUrlList = urlRss.Split(',');
            var lstData = new List<object>();
            foreach(var urlFeed in FeedUrlList)
            {
                XDocument feedXML = XDocument.Load(urlFeed);
                var feeds = from feed in feedXML.Descendants("item")
                select new
                {
                    Title = feed.Element("title").Value,
                    Link = feed.Element("link").Value,
                    Description = feed.Element("description").Value
                };
                lstData.AddRange(feeds);
            }
            

            return lstData;
        }
    }
}
