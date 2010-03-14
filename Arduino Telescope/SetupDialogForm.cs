using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ASCOM.Arduino
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        ASCOM.Utilities.Profile profile = new ASCOM.Utilities.Profile();

        public SetupDialogForm()
        {
            profile.DeviceType = "Telescope";
            InitializeComponent();

            this.comboComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            this.comboComPort.SelectedItem = profile.GetValue(ASCOM.Arduino.Telescope.s_csDriverID, "ComPort");
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            profile.WriteValue(ASCOM.Arduino.Telescope.s_csDriverID, "ComPort", this.comboComPort.SelectedItem.ToString());
            Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BrowseToAscom(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}