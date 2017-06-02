using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckConnection.Workflow
{
    public partial class UserForm : Form
    {
        public int Checked = 1;
        public UserForm()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Checked = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Checked = 2;
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
