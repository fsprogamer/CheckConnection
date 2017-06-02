using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class LogForm : GenericForm
    {
        ListBox lbox;
        List<string> _lst;//= new List<string>();

        public LogForm(string[] lst)
        {
            InitializeComponent();
            _lst = lst.ToList<string>();
        }

        private new void FillForm()
        {
            lbox= new ListBox();
            lbox.AutoSize = true;
            lbox.Dock = DockStyle.Fill;
            flpanel.Controls.Add(lbox);
            Info();
        }

        public void Info(/*string mess*/)
        {
            //lst.Add(mess);
            lbox.DataSource = _lst;
            lbox.Refresh();
        }

        private void ShowMessForm_Load(object sender, EventArgs e)
        {
            FillForm();
        }
    }
}
