using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckConnection
{
    public partial class TestIpTextBox : Form
    {
        public TestIpTextBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip_str1 = ipAddressControl1.Text;
            MessageBox.Show(ip_str1, "", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            string ip_str2 = ipAddressControl2.Text;
            MessageBox.Show(ip_str2, "", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            if (ipAddressControl1.Text != "...")
                MessageBox.Show("ipAddressControl1.Text!=\"...\"", "", MessageBoxButtons.OK,
        MessageBoxIcon.Information);
        }
    }
}
