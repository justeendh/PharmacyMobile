using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using CommonLibs;
using System.Collections;
using System.Text.RegularExpressions;
using GsmComm.PduConverter;
using NAudio.Wave;
using System.Threading.Tasks;

namespace ModemModule
{
    public class GSMModemDevice
    {
        protected SafeSerialPort SpGSM_Modem;
        protected SafeSerialPort SpGSM_Modem_AutoResponse;
        protected SafeSerialPort SpGSM_Modem_Speech;
        protected AutoResetEvent receiveNow;
        protected AutoResetEvent receive_AutoResponse;
        protected AutoResetEvent receive_Speech;
        
        private BufferedWaveProvider _bufferedWaveProvider;
        private WaveOut _waveOut;
        private WaveIn _waveIn;

        public string PortName { set; get; }
        public int BaudRate { set; get; }
        public int DataBits { set; get; }
        public StopBits Stopbits { set; get; }
        public Parity Parity { set; get; }

        public string Carrier { set; get; }
        public string DeviceManufacturers { set; get; }
        public string DeviceModel { set; get; }
        public string DeviceRevision { set; get; }


        public GSMModemDevice(string _PortName, int _BaudRate, int _DataBit, StopBits _stopbit, Parity _parity)
        {
            receiveNow = new AutoResetEvent(false);
            receive_AutoResponse = new AutoResetEvent(false);

            this.PortName = _PortName;
            this.BaudRate = _BaudRate;
            this.DataBits = _DataBit;
            this.Stopbits = _stopbit;
            this.Parity = _parity;

            //SerialPort
            SpGSM_Modem = new SafeSerialPort();
            SpGSM_Modem.PortName = _PortName;
            SpGSM_Modem.BaudRate = this.BaudRate;
            SpGSM_Modem.DataBits = this.DataBits;
            SpGSM_Modem.StopBits = this.Stopbits;
            SpGSM_Modem.Parity = this.Parity;
            SpGSM_Modem.WriteTimeout = 500;
            SpGSM_Modem.Encoding = Encoding.GetEncoding("iso-8859-1");
            SpGSM_Modem.DataReceived += port_DataReceived;
            SpGSM_Modem.PinChanged += SpGSM_Modem_PinChanged;
            SpGSM_Modem.ErrorReceived += SpGSM_Modem_ErrorReceived;
            SpGSM_Modem.DtrEnable = true;
            SpGSM_Modem.RtsEnable = true;
        }

        private void SpGSM_Modem_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Console.WriteLine(string.Format("{0}: Serial port ErrorReceived. {1}", DateTime.Now, e.ToString()));
        }

        private void SpGSM_Modem_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Console.WriteLine(string.Format("{0}: Serial port PinChanged. {1}", DateTime.Now, e.ToString()));
        }

        public bool OpenPort(bool ResetConfig = false)
        {
            try
            {
                if (ResetConfig)
                {
                    if (SpGSM_Modem != null && SpGSM_Modem.IsOpen) SpGSM_Modem.Close();
                    SpGSM_Modem.PortName = this.PortName;
                    SpGSM_Modem.Open();
                    return SpGSM_Modem.IsOpen;
                }
                else
                {
                    if (!SpGSM_Modem.IsOpen) SpGSM_Modem.Open();
                    return SpGSM_Modem.IsOpen;
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }


        public bool IsOpenPort()
        {
            return SpGSM_Modem.IsOpen;
        }

        public void ClosePort()
        {
            receiveNow.Reset();
            if (SpGSM_Modem != null && SpGSM_Modem.IsOpen) SpGSM_Modem.Close();
        }


        public bool OpenAutoResponsePort(string PortName)
        {
            try
            {
                //source = new CancellationTokenSource();
                //CancellationToken token = source.Token;
                //taskWaitResponse = Task.Factory.StartNew(() => {
                    
                //}, token);

                //SerialPort
                SpGSM_Modem_AutoResponse = new SafeSerialPort();
                SpGSM_Modem_AutoResponse.BaudRate = this.BaudRate;
                SpGSM_Modem_AutoResponse.DataBits = this.DataBits;
                SpGSM_Modem_AutoResponse.StopBits = this.Stopbits;
                SpGSM_Modem_AutoResponse.Parity = this.Parity;
                SpGSM_Modem_AutoResponse.WriteTimeout = 500;
                SpGSM_Modem_AutoResponse.Encoding = Encoding.GetEncoding("iso-8859-1");
                SpGSM_Modem_AutoResponse.DataReceived += portAutoResponse_DataReceived;
                SpGSM_Modem_AutoResponse.DtrEnable = true;
                SpGSM_Modem_AutoResponse.RtsEnable = true;

                if (SpGSM_Modem_AutoResponse != null && SpGSM_Modem_AutoResponse.IsOpen) SpGSM_Modem_AutoResponse.Close();
                SpGSM_Modem_AutoResponse.PortName = PortName;
                SpGSM_Modem_AutoResponse.Open();
                return SpGSM_Modem_AutoResponse.IsOpen;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }

        public void CloseAutoResponsePort()
        {
            receive_AutoResponse.Reset();
            if (SpGSM_Modem_AutoResponse != null && SpGSM_Modem_AutoResponse.IsOpen) SpGSM_Modem_AutoResponse.Close();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (SpGSM_Modem_Speech.IsOpen)
            {
                SpGSM_Modem_Speech.Write(e.Buffer, 0, e.BytesRecorded);
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (_waveIn != null)
            {
                _waveIn.Dispose();
                _waveIn = null;
            }
        }

        public bool OpenSpeechPort(string PortName)
        {
            try
            {
                _bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(16000, 1));
                _waveOut = new WaveOut();
                PlayAudio();

                //SerialPort
                SpGSM_Modem_Speech = new SafeSerialPort();
                SpGSM_Modem_Speech.BaudRate = this.BaudRate;
                SpGSM_Modem_Speech.DataBits = this.DataBits;
                SpGSM_Modem_Speech.StopBits = this.Stopbits;
                SpGSM_Modem_Speech.Parity = this.Parity;
                SpGSM_Modem_Speech.WriteTimeout = 1000;
                SpGSM_Modem_Speech.Encoding = Encoding.GetEncoding("iso-8859-1");
                SpGSM_Modem_Speech.DataReceived += portSpeech_DataReceived;
                SpGSM_Modem_Speech.DtrEnable = true;
                SpGSM_Modem_Speech.RtsEnable = true;
                SpGSM_Modem_Speech.ReadBufferSize = 100000;
                SpGSM_Modem_Speech.WriteBufferSize = 100000;

                if (SpGSM_Modem_Speech != null && SpGSM_Modem_Speech.IsOpen) SpGSM_Modem_Speech.Close();
                SpGSM_Modem_Speech.PortName = PortName;
                SpGSM_Modem_Speech.Open();

                _waveIn = new WaveIn();
                _waveIn.WaveFormat = new WaveFormat(16000, 1);
                _waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
                _waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
                _waveIn.StartRecording();

                return SpGSM_Modem_Speech.IsOpen;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }
        
        public void CloseSpeechPort()
        {
            receive_AutoResponse.Reset();
            if (SpGSM_Modem_Speech != null && SpGSM_Modem_Speech.IsOpen) SpGSM_Modem_Speech.Close();
            StopPlayAudio();
            _waveIn.StopRecording();
        }


        protected void PlayAudio()
        {
            _waveOut.Init(_bufferedWaveProvider);
            _waveOut.Play();
        }

        protected void StopPlayAudio()
        {
            _waveOut.Stop();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                receiveNow.Set();
            }
        }

        private void portAutoResponse_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                receive_AutoResponse.Set();
            }
        }

        private void portSpeech_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int byteReadCurrent = SpGSM_Modem_Speech.BytesToRead;
            byte[] buffering = new byte[byteReadCurrent];
            SpGSM_Modem_Speech.Read(buffering, 0, byteReadCurrent);
            _bufferedWaveProvider.AddSamples(buffering, 0, buffering.Length);
        }

        public bool IsOpened()
        {
            return SpGSM_Modem.IsOpen;
        }

        public bool CheckOK(string buffer)
        {
            if (buffer.EndsWith("\r\nOK\r\n")) return true;
            else return false;
        }

        public bool CheckERROR(string buffer)
        {
            if (buffer.EndsWith("\r\nERROR\r\n")) return true;
            else return false;
        }

        public bool CheckNEXT(string buffer)
        {
            if (buffer.EndsWith("\r\n> ")) return true;
            else return false;
        }

        #region common Function

        public string ExecuteCommands(string ATcommand, int timeout, string Error = "")
        {
            receiveNow.Reset();
            try
            {
                string result = "";
                SpGSM_Modem.Write(ATcommand + "\r");
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = SpGSM_Modem.ReadExisting();
                        result += t;
                    }
                    else
                    {
                        string t = SpGSM_Modem.ReadExisting();
                        result += t;
                        return result;
                    }
                }
                while (!result.EndsWith("\r\nOK\r\n") && !result.EndsWith("\r\n> ") && !result.EndsWith("\r\nERROR\r\n"));
                return result;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return ex.Message;
            }
        }

        public string WaitingData(int timeout, string ResultContain = null, string Error = "")
        {
            receive_AutoResponse.Reset();
            try
            {
                string result = "";
                do
                {
                    if (receive_AutoResponse.WaitOne(timeout, false))
                    {
                        string t = SpGSM_Modem_AutoResponse.ReadExisting();
                        result += t;
                    }
                    else
                    {
                        string t = SpGSM_Modem_AutoResponse.ReadExisting();
                        result += t;
                        return result;
                    }
                }
                while ((!result.EndsWith("\r\nOK\r\n") && !result.EndsWith("\r\n> ") && !result.EndsWith("\r\nERROR\r\n") && !result.EndsWith("\r\n") && !result.EndsWith("\r"))
                        && ((ResultContain != null && result.Contains(ResultContain)) || ResultContain == null));
                return result;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return string.Empty;
            }
        }
        

        public void InitModemConfig()
        {
            string response = ExecuteCommands("ATE0", 500);
            response = ExecuteCommands("AT+CSCS=\"GSM\"", 500);
            response = ExecuteCommands("AT+CMGF=0", 500);
            response = ExecuteCommands("AT+CNMI=0,0,0,0,0", 500);
            response = ExecuteCommands("AT+CSCS=\"GSM\"", 500);
        }

        public bool setModemSIMReady()
        {
            string response = ExecuteCommands("AT+CFUN?", 500);
            if (response.Contains("+CFUN: 0")) ExecuteCommands("AT+CFUN=1", 500);
            response = ExecuteCommands("AT+CPIN?", 500);
            if (response.Contains("READY")) return true;
            else return false;
        }

        protected string GetManufacturersName()
        {
            string NSX = ExecuteCommands("AT+CGMI", 500);
            StringBuilder result = new StringBuilder();
            Regex r = new Regex(@" (.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\r\n(.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Match m = r.Match(NSX);
            Match m2 = r2.Match(NSX);
            if (m.Success || m2.Success)
            {
                if (m.Success) result.Append(m.Groups[1].Value);
                else result.Append(m2.Groups[1].Value);
            }
            return result.ToString();
        }

        protected string GetModelName()
        {
            string Model = ExecuteCommands("AT+CGMM", 500);
            StringBuilder result = new StringBuilder();
            Regex r = new Regex(@" (.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\r\n(.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Match m = r.Match(Model);
            Match m2 = r2.Match(Model);
            if (m.Success || m2.Success)
            {
                if (m.Success) result.Append(m.Groups[1].Value);
                else result.Append(m2.Groups[1].Value);
            }
            return result.ToString();
        }

        protected string GetRevisionName()
        {
            string Revision = ExecuteCommands("AT+CGMR", 500);
            StringBuilder result = new StringBuilder();
            Regex r = new Regex(@" (.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\r\n(.+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Match m = r.Match(Revision);
            Match m2 = r2.Match(Revision);
            if (m.Success || m2.Success)
            {
                if (m.Success) result.Append(m.Groups[1].Value);
                else result.Append(m2.Groups[1].Value);
            }
            return result.ToString();
        }

        
        protected string GetCallIdNumber(string modemResponse)
        {
            StringBuilder result = new StringBuilder();
            Regex r = new Regex(@"\+CLIP: ""(\d +)"",(\d+),"""",(\d),"""",(\d)\r\nOK\r\n", RegexOptions.Singleline);
            Match m = r.Match(modemResponse);
            if (m.Success)
            {
                result.Append(m.Groups[1].Value);
            }
            return result.ToString();
        }
        
        protected string GetContentResponse(string buffer)
        {
            StringBuilder result = new StringBuilder(buffer);
            int indexrm = buffer.IndexOf("\r\r\n");
            result.Remove(0, indexrm + 3);
            result.Replace("\r\n\r\nOK\r\n", "");
            result.Replace("\r\n\r\nERROR\r\n", "");
            result.Replace("\r\n\r\n", " ");
            result.Replace("\r\n", " ");
            return result.ToString();
        }
        
        protected ShortMessage NewSmsGetInfo(string input)
        {
            StringBuilder rs = new StringBuilder();
            Regex r = new Regex(@"\+CMT: ""(.+)"",(.*),""(.+)""\r\n(.+)\r\n", RegexOptions.Singleline);
            Match m = r.Match(input);

            ShortMessage msg = new ShortMessage();
            if (m.Success)
            {
                //msg.Index = int.Parse(m.Groups[1].Value);
                //msg.Status = m.Groups[1].Value;
                msg.Sender = m.Groups[1].Value;
                msg.Alphabet = m.Groups[2].Value;
                msg.Sent = m.Groups[3].Value;
                msg.Message = m.Groups[4].Value;
                return msg;
            }
            return null;
        }

        protected int NewSmsGetIndex(string input)
        {
            int rs = 0;
            Regex r = new Regex(@"\+CMTI: ""(.+)"",(\d+)\r\n", RegexOptions.Singleline);
            Match m = r.Match(input);
            if (m.Success)
            {
                rs = int.Parse(m.Groups[2].Value);
                return rs;
            }
            return 0;
        }

        protected string GetCarrier()
        {
            string CarrierName = this.ExecuteCommands("AT+COPS?", 1000);
            StringBuilder rs = new StringBuilder();
            Regex r = new Regex(@"\+COPS: (\d+),(\d+),""(.+)"",(\d+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\+COPS: (\d+), (\d+), ""(.+)"", (\d+)\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Regex r3 = new Regex(@"\+COPS: (\d+),(\d+),""(.+)""\r\n\r\nOK\r\n", RegexOptions.Singleline);
            Match m = r.Match(CarrierName);
            Match m2 = r2.Match(CarrierName);
            Match m3 = r3.Match(CarrierName);
            if (m.Success || m2.Success || m3.Success)
            {
                if (m.Success) rs.Append(m.Groups[3].Value);
                if (m2.Success) rs.Append(m2.Groups[3].Value);
                if (m3.Success) rs.Append(m3.Groups[3].Value);
            }

            return rs.ToString();
        }

        protected string UssdResult(string input)
        {
            StringBuilder rs = new StringBuilder();
            Regex r = new Regex(@"\+CUSD: (\d+),""(.+)"",(\d+)\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\+CUSD: (\d+), ""(.+)"", (\d+)\r\n", RegexOptions.Singleline);
            Match m = r.Match(input);
            Match m2 = r2.Match(input);
            if (m.Success || m2.Success)
            {
                if (m.Success) rs.Append(m.Groups[2].Value);
                else rs.Append(m2.Groups[2].Value);
            }

            return rs.ToString();
        }
        
        protected void GetDeviceInfo()
        {
            this.DeviceManufacturers = GetManufacturersName();
            this.DeviceModel = GetModelName();
            this.DeviceRevision = GetRevisionName();

            try
            {
                Dictionary<string, string> Operator = new Dictionary<string, string>();
                Operator.Add("45201", "VN MobiFone");
                Operator.Add("45202", "VN Vinaphone");
                Operator.Add("45204", "VN Viettel");
                Operator.Add("45205", "Vietnam Mobile");
                string carrierId = GetCarrier();
                if (Operator.ContainsKey(carrierId)) this.Carrier = Operator[carrierId];
                else this.Carrier = GetCarrier();
            }
            catch { }
        }

        public static ShortMessageCollection GetMessenge(string input)
        {
            ShortMessageCollection messages = new ShortMessageCollection();
            Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n", RegexOptions.Singleline);
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

        public static ShortMessage GetInboxMessenge(string input, int index)
        {
            Regex r = new Regex(@"\+CMGR: ""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n\r\n", RegexOptions.Singleline);
            Match m = r.Match(input);
            if (m.Success)
            {
                ShortMessage msg = new ShortMessage();
                msg.Index = index;
                msg.Status = m.Groups[1].Value;
                msg.Sender = m.Groups[2].Value;
                msg.Alphabet = m.Groups[3].Value;
                msg.Sent = m.Groups[4].Value;
                msg.Message = m.Groups[5].Value;

                return msg;
            }

            return null;
        }

        #endregion

        public bool OpenModem(out string msg)
        {
            try
            {
                if (!OpenPort())
                {
                    msg = "GSM Modem can not connect. Please check again";
                    return false;
                }
                else
                {
                    msg = "";
                    string response = ExecuteCommands("AT", 1000);
                    if (!CheckOK(response))
                    {
                        msg = "GSM Modem does not respond to commands. or the device is not a GSM modem";
                        ClosePort();
                        return false;
                    }

                    InitModemConfig();
                    msg = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                msg = "There was an unknown error. Please check and try again";
                return false;
            }
        }
        
        
        public static Dictionary<string, GSMModemDevice>  SearchModem()
        {
            try
            {
                int[] arrayBaudrate = new int[] { 115200 };
                Dictionary<string, GSMModemDevice> lstResultDevices = new Dictionary<string, GSMModemDevice>();
                List<string> ModemList = CommonFunctions.GetListModem();
                List<string> PortListChecked = new List<string>();
                foreach (string port in ModemList)
                {
                    if (PortListChecked.Contains(port)) continue;
                    foreach ( int baudrateCheck in arrayBaudrate)
                    {
                        GSMModemDevice modemcom = new GSMModemDevice(port, baudrateCheck, 8, StopBits.One, Parity.None);
                        if (!modemcom.OpenPort())
                        {
                            modemcom.ClosePort();
                            modemcom.Dispose();
                            continue;
                        }

                        string response = modemcom.ExecuteCommands("AT", 500);
                        if (!modemcom.CheckOK(response))
                        {
                            modemcom.ClosePort();
                            modemcom.Dispose();
                            continue;
                        }
                        modemcom.InitModemConfig();
                        if (!modemcom.setModemSIMReady())
                        {
                            modemcom.ClosePort();
                            modemcom.Dispose();
                            continue;
                        }

                        response = modemcom.ExecuteCommands("AT+GCAP", 200);
                        if (!response.Contains("GSM"))
                        {
                            modemcom.ClosePort();
                            modemcom.Dispose();
                            continue;
                        }

                        modemcom.GetDeviceInfo();
                        modemcom.ClosePort();
                        if (!lstResultDevices.ContainsKey(port)) lstResultDevices.Add(DateTime.Now.ToString("ddMMyyyyHHmmss"), modemcom);
                        if(!PortListChecked.Contains(port)) PortListChecked.Add(port);
                        Thread.Sleep(200);
                    }
                }
                return lstResultDevices;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return null;
            }
        }

        protected string USSDResult(string input)
        {
            StringBuilder rs = new StringBuilder();
            Regex r = new Regex(@"\+CUSD: (\d+),""(.+)"",(\d+)\r\n", RegexOptions.Singleline);
            Regex r2 = new Regex(@"\+CUSD: (\d+), ""(.+)"", (\d+)\r\n", RegexOptions.Singleline);
            Match m = r.Match(input);
            Match m2 = r2.Match(input);
            if (m.Success || m2.Success)
            {
                if (m.Success) rs.Append(m.Groups[2].Value);
                else rs.Append(m2.Groups[2].Value);
            }

            return rs.ToString();
        }

        //Send USSD
        public string SendUSSD(string Code)
        {
            try
            {
                string USSDMsg = string.Empty;
                string USSD_Code = "AT+CUSD=1,\"" + Code + "\",15";
                string response = ExecuteCommands(USSD_Code, 500);
                if (CheckOK(response))
                {
                    string resultStr = WaitingData(15000, "+CUSD");
                    //Nếu hỗ trợ định dạng plain-text thì vào đây làm việc
                    string rs = USSDResult(resultStr);
                    if (rs.Trim().Replace("\r", "").Replace("\n", "") == "")
                    {
                        LoggingData.WriteLog("Gửi USSD không thành công. Modem không phản hồi lệnh.");
                        USSDMsg = "Gửi USSD không thành công. Modem không phản hồi lệnh.";
                        return USSDMsg;
                    }
                    USSDMsg = rs;
                    return rs;
                }
                else
                {
                    //Nếu không hỗ trợ Text, thử với định dạng endcode 7bit
                    string EndCodeUSSDCode = TextDataConverter.SeptetsToOctetsHex(Code);
                    USSD_Code = "AT+CUSD=1,\"" + EndCodeUSSDCode + "\",15";
                    response = ExecuteCommands(USSD_Code, 500);

                    if (CheckOK(response))
                    {
                        string resultStr = WaitingData(15000);
                        string HexString = USSDResult(resultStr);
                        if (HexString.Trim().Replace("\r", "").Replace("\n", "") == "")
                        {
                            LoggingData.WriteLog("Gửi USSD không thành công. Modem không phản hồi lệnh.");
                            USSDMsg = "Gửi USSD không thành công. Modem không phản hồi lệnh.";
                            return USSDMsg;
                        }
                        string USSDResultMsg = GSMConverter.Decode7BitText(HexString);
                        USSDMsg = USSDResultMsg;
                        return USSDMsg;
                    }
                    else
                    {
                        LoggingData.WriteLog("Gửi USSD không thành công. Modem không phản hồi lệnh.");
                        USSDMsg = "Gửi USSD không thành công. Modem không phản hồi lệnh.";
                        return USSDMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                string USSDMsg = ex.Message;
                return USSDMsg;
            }
        }


        private bool SendSMS(string Number, string Content, byte dcs)
        {
            try
            {
                SmsSubmitPdu sms = new SmsSubmitPdu(Content, Number, dcs);
                string pdu = sms.ToString(false);
                string response = ExecuteCommands("AT+CMGS=" + sms.ActualLength.ToString() + "", 1000);
                if (!CheckNEXT(response))
                {
                    LoggingData.WriteLog("Send SMS failed. GSM Modem does not respond to commands");
                    return false;
                }
                response = ExecuteCommands(pdu + char.ConvertFromUtf32(26), 10000);
                if (!CheckOK(response))
                {
                    LoggingData.WriteLog("Send SMS failed. GSM Modem does not respond to commands");
                    return false;
                };
                return true;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }

        public bool SendLongSMS(string Number, string Content)
        {
            try
            {
                int chunkSize = 70;
                byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha16Bit;
                if (!CommonFunctions.ContainsUnicodeCharacter(Content))
                {
                    chunkSize = 140;
                    dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
                }
                IEnumerable<string> multipart = CommonFunctions.SmartSplit(Content, chunkSize);
                int Success = 0;
                foreach(var msg in multipart)
                {
                    bool rs = SendSMS(Number, msg, dcs);
                    if (rs) Success++;
                    Thread.Sleep(300);
                }
                if (Success == multipart.Count()) return true;
                else return false;
            }
            catch (Exception ex)
            {
                LoggingData.WriteLog(ex);
                return false;
            }
        }

        public void Dispose()
        {
            SpGSM_Modem.Dispose();
        }

        public override string ToString()
        {
            try
            {
                return string.Format("{0}: Manufacturers: {1} / Model: {2} / Revision: {3} / Network: {4}", this.PortName, this.DeviceManufacturers, this.DeviceModel, this.DeviceRevision, this.Carrier);
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
