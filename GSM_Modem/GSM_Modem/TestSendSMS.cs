using ModemModule;
using ModemModule.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSM_Modem
{
    public partial class TestSendSMS : FormModemTask
    {
        ModemTask modemTask;
        ModemComunication modem;

        public TestSendSMS()
        {
            InitializeComponent();
        }

        private void TestSendSMS_Load(object sender, EventArgs e)
        {
            try
            {
                modemTask = ModemTask.GetInstance(this);
            }
            catch(Exception ex)
            {

            }
        }

        public override void OnSMSReceived(ShortMessage msg)
        {
            base.OnSMSReceived(msg);
            txtResponse.Text += "Tin nhắn từ " + msg.Sender + ", nội dung: " + msg.Message;
        }

        public override void OnSendSMSCompleated(int Success, int Fail)
        {
            base.OnSendSMSCompleated(Success, Fail);
            string append = "";
            append = ", Thất bại " + Fail.ToString() + " tin nhắn";
            lbStatus.Text = "Gửi tin nhắn thành công " + Success + " tin nhắn" + append;
        }

        public override void OnSendUSSDCompleated(bool Success, string Response)
        {
            base.OnSendUSSDCompleated(Success, Response);
            if (Success)
            {
                txtResponse.Text += Response + "\r\n";
            }
            else MessageBox.Show("Truy vấn thất bại hoặc không đúng. Vui lòng thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override void OnNotification(NOTIFICATIONS_TYPE msg)
        {
            base.OnNotification(msg);
            if (msg == NOTIFICATIONS_TYPE.MODEM_BUSY)
            MessageBox.Show("Có tác vụ đang cần xử lý. Vui lòng chờ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            modemTask.SendSMS(txtPhone.Text, txtSMS.Text);
        }

        private void btnSendUSSD_Click(object sender, EventArgs e)
        {
            modemTask.SendUSSD("*101#");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<NewShortMessenge> lstToSend = new List<NewShortMessenge>();
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 1 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 2 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 3 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 4 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 5 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 6 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 7 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 8 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 9 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 10 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 11 la"));
            lstToSend.Add(new NewShortMessenge("0985930542", "Luong thang 12 la"));
            modemTask.SendSMSList(lstToSend);
        }

        private void btnReadSMS_Click(object sender, EventArgs e)
        {
            modemTask.ReadAllSMS();
        }

        public override void OnReadAllSMSCompleated(ShortMessageCollection smsCollection)
        {
            base.OnReadAllSMSCompleated(smsCollection);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            modemTask.DeleteAllSMS();
        }

        public override void OnDeleteSMS(int Success, int Fail)
        {
            base.OnSendSMSCompleated(Success, Fail);
            string append = "";
            append = ", " + Fail.ToString() + " tin nhắn xoá thất bại";
            lbStatus.Text = "Đã xoá " + Success.ToString() +  " tin nhắn thành công tin nhắn" + append;
        }
    }
}
