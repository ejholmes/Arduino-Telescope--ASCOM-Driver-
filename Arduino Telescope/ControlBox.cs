using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ASCOM;
using ASCOM.Helper;
using ASCOM.Helper2;
using ASCOM.Interface;
using ASCOM.Utilities;

namespace ASCOM.Arduino
{
    public partial class ControlBox : Form
    {
        public ControlBox(ASCOM.Arduino.Telescope scope)
        {
            this.telescope = scope;
            InitializeComponent();
        }

        void Halt(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            telescope.AbortSlew();
        }

        private void buttonNorth_Down(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            telescope.MoveAxis(TelescopeAxes.axisSecondary, 1);
        }
        private void buttonSouth_Down(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            telescope.MoveAxis(TelescopeAxes.axisSecondary, -1);
        }
        private void buttonEast_Down(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            telescope.MoveAxis(TelescopeAxes.axisPrimary, 1);
        }
        private void buttonWest_Down(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            telescope.MoveAxis(TelescopeAxes.axisPrimary, -1);
        }
    }
}
