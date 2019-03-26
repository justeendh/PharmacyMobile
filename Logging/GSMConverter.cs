using GsmComm.PduConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibs
{
    public static class GSMConverter
    {
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        
        public static string Decode7BitText(string HexData)
        {
            byte[] data = GSMConverter.StringToByteArray(HexData);
            string decodestring = TextDataConverter.OctetsToSeptetsStr(data);
            return decodestring;
        }

        public static string EncodePhoneNumber(string PhoneNumber)
        {
            string result = "";
            if ((PhoneNumber.Length % 2) > 0) PhoneNumber += "F";

            int i = 0;
            while (i < PhoneNumber.Length)
            {
                result += PhoneNumber[i + 1].ToString() + PhoneNumber[i].ToString();
                i += 2;
            }
            return result.Trim();
        }

        public static string StringToUCS2(string str)
        {
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] ucs2 = ue.GetBytes(str);

            int i = 0;
            while (i < ucs2.Length)
            {
                byte b = ucs2[i + 1];
                ucs2[i + 1] = ucs2[i];
                ucs2[i] = b;
                i += 2;
            }
            return BitConverter.ToString(ucs2).Replace("-", "");
        }

        public static string GetPDUSMS(string telnumber, string textSMS, out string lengthPDUSMS)
        {
            string _telnumber = "01" + "00" + telnumber.Length.ToString("X2") + "81" + EncodePhoneNumber(telnumber);
            string _textSMS = StringToUCS2(textSMS);
            string leninByte = (_textSMS.Length / 2).ToString("X2");
            string PDUText = _telnumber + "00" + "0" + "8" + leninByte + _textSMS;
            double lenMes = PDUText.Length / 2;
            lengthPDUSMS = (Math.Ceiling(lenMes)).ToString();
            PDUText = "00" + PDUText;
            return PDUText + char.ConvertFromUtf32(26);
        }

        //public static string StringTo7Bit(string s)
        //{

        //    Encoding gsmEnc = new Mediaburst.Text.GSMEncoding();
        //    Encoding utf8Enc = new System.Text.UTF8Encoding();

        //    byte[] utf8Bytes = Encoding.Convert(gsmEnc, utf8Enc, gsmBytes);
        //    string result = utf8Enc.GetString(utf8Bytes);
        //}
    }
}
