using System;
using System.Windows.Forms;

namespace CheckConnection
{
    public partial class RepairForm : Form
    {
        string[] _text;
        public RepairForm(string[] text)
        {
            InitializeComponent();
            _text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RepairForm_Load(object sender, EventArgs e)
        {
            
        }

        private void RepairForm_Shown(object sender, EventArgs e)
        {
            WorkflowLib.WorkFlowApp.Run(_text);
        }
    }
}
