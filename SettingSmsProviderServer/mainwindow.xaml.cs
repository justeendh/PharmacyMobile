using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using ModemModule;
using CommonLibs;

namespace SettingSmsProviderServer
{
    /// <summary>
    /// Interaction logic for MainWindows.xaml
    /// </summary>
    public partial class MainWindows : Window
    {
        GSMModemDevice modem;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void move_window(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public MainWindows()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbDeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            modem = cbDeviceList.SelectedItem as GSMModemDevice;
        }

        private void btnSearchModem_Click(object sender, RoutedEventArgs e)
        {
            cbDeviceList.Items.Clear();
            Dictionary<string, GSMModemDevice> lstResultSearch = GSMModemDevice.SearchModem();
            if (lstResultSearch != null && lstResultSearch.Count > 0)
            {
                foreach (var item in lstResultSearch)
                {
                    cbDeviceList.Items.Add(item.Value);
                }
                cbDeviceList.SelectedIndex = 0;
            }
            else MessageBox.Show("No GSM modem device found", "Infomations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Save settings", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(modem != null)
                {
                    try
                    {
                        Dictionary<string, object> dictData = new Dictionary<string, object>();
                        dictData.Add("PORT_NAME", modem.PortName);
                        dictData.Add("COM_BAUDRATE", modem.BaudRate);
                        dictData.Add("COM_PARITY", modem.Parity);
                        dictData.Add("COM_STOP_BIT", modem.Stopbits);
                        dictData.Add("COM_DATA_BIT", modem.DataBits);
                        CommonFunctions.SaveSerializedObject(dictData, "GsmSettings.dat");
                        MessageBox.Show("Save settings successfully", "Infomations", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Save settings failed. Please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Save settings failed. Please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
