using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System;
using System.Windows.Forms;
using log4net;
using Common;

namespace CheckConnection
{
    public partial class ModeForm: BaseForm
    {        
        public ModeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // hide main form
            this.Hide();

            var DisplayConn = new DisplayConnections();
            DisplayConn.StartPosition = FormStartPosition.WindowsDefaultLocation;
            DisplayConn.Mode = (radioButtonDiagnose.Checked == true) ? 1 : 0;
            // show other form            
            DisplayConn.ShowDialog();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
