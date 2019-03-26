using CommonLibs;
using ModemModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestModem
{
    
   

    public partial class Form1 : Form
    {    
        GSMModemDevice modem;
        public Form1()
        {
            InitializeComponent();
        }

        public static Dictionary<string, SerialPortItem> GetDictSerialPort()
        {
            Dictionary<string, SerialPortItem> PortsResult = new Dictionary<string, SerialPortItem>();
            string[] lstPort = SerialPort.GetPortNames();
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem"))
            {
                if (searcher.Get() != null && searcher.Get().Count > 0)
                {

                    foreach (ManagementObject item in searcher.Get())
                    {
                        PortsResult.Add(item["AttachedTo"].ToString(), new SerialPortItem() { Description = item["Caption"].ToString() + " " + item["AttachedTo"].ToString(), PortName = item["AttachedTo"].ToString() });
                    }
                    
                }
            }
            return PortsResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Dictionary<string, GSMModemDevice> lstResultSearch = GSMModemDevice.SearchModem();
            //if(lstResultSearch != null && lstResultSearch.Count > 0)
            //{
            //    foreach(var item in lstResultSearch)
            //    {
            //        comboBox1.Items.Add(item.Value);
            //    }
            //}
            Dictionary<string, SerialPortItem> listResponsePort = GetDictSerialPort();
            if (listResponsePort != null && listResponsePort.Count > 0)
            {
                foreach (var item in listResponsePort)
                {
                    comboBox2.Items.Add(item.Value);
                }
            }
            MessageBox.Show("Done");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = modem.ExecuteCommands(textBox1.Text, 500);
            textBox2.AppendText(result + Environment.NewLine);
            if (checkBox1.Checked)
            {
                result = modem.WaitingData(10000);
                textBox2.AppendText(result + Environment.NewLine);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cusd = modem.SendUSSD("*101#");
            textBox2.AppendText(cusd.Trim() + Environment.NewLine);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (modem != null)
            {
                modem.ClosePort();
                modem.CloseAutoResponsePort();
            }
            modem = comboBox1.SelectedItem as GSMModemDevice;
            SerialPortItem responsePort = comboBox2.SelectedItem as SerialPortItem;
            if(modem != null) modem.OpenPort();
            if (responsePort != null) modem.OpenAutoResponsePort(responsePort.PortName);
            MessageBox.Show("Done");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cusd =  modem.SendUSSD("*102#");
            textBox2.AppendText(cusd.Trim() + Environment.NewLine);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string cusd = modem.SendUSSD("*0#");
            textBox2.AppendText(cusd.Trim() + Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modem != null)
            {
                modem.ClosePort();
                modem.CloseAutoResponsePort();
                modem.CloseSpeechPort();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (modem != null)
            {
                modem.OpenSpeechPort("COM9");
            }
            MessageBox.Show("Done");
        }
    }
}
