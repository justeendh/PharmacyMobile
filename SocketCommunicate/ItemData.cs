using CommonLibs;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SocketCommunicate
{
    [Serializable]
    public class ItemData
    {
        public string Action;
        public object StateObject;
        
        public byte[] GetBytes()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, this);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return null;
            }
        }

        public static ItemData Parse(byte[] arrBytes)
        {
            try
            {
                BinaryFormatter binForm = new BinaryFormatter();
                using (MemoryStream memStream = new MemoryStream())
                {
                    memStream.Write(arrBytes, 0, arrBytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    ItemData result = (ItemData)binForm.Deserialize(memStream);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return null;
            }
        }
    }
}
