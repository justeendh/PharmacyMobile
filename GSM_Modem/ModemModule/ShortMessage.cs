using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ModemModule
{
    public class ShortMessage
    {
        #region Private Variables
        private int index;
        private string status;
        private string sender;
        private string alphabet;
        private string sent;
        private string message;
        #endregion

        #region Public Properties
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; }
        }
        public string Sent
        {
            get { return sent; }
            set { sent = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion
    }

    public class ShortMessageCollection:List<ShortMessage>
    {
        public static ShortMessageCollection ParseMessages(string input)
        {
            ShortMessageCollection messages = new ShortMessageCollection();
            Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
            Match m = r.Match(input);
            while (m.Success)
            {
                ShortMessage msg = new ShortMessage();
                msg.Index = int.Parse(m.Groups[1].Value);
                msg.Status = m.Groups[2].Value;
                msg.Sender = m.Groups[3].Value;
                msg.Alphabet = m.Groups[4].Value;
                msg.Sent = m.Groups[5].Value;
                msg.Message = m.Groups[6].Value;
                messages.Add(msg);

                m = m.NextMatch();
            }

            return messages;
        }
    }
}
