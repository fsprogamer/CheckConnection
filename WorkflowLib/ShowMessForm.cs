using System;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class ShowMessForm : GenericForm
    {
        private string _text = String.Empty;
        public ShowMessForm(string text)
        {
            _text = text;
            InitializeComponent();
        }

        private new void FillForm()
        {  
            Label Textlabel = new Label();
            Textlabel.AutoSize = true;            
            Textlabel.Dock = DockStyle.Fill;
            Textlabel.Text = _text;
            flpanel.Controls.Add(Textlabel);           
        }

        private void ShowMessForm_Load(object sender, EventArgs e)
        {
            FillForm();
        }
    }
}