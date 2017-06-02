using System;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class MakeChoiceForm : GenericForm
    {
        private string[] /*List<KeyValuePair<int, string>>*/ _valuelist;
        public int Checked = 0;
        public MakeChoiceForm(string[] /*List<KeyValuePair<int, string>>*/ valuelist)
        {
            _valuelist = valuelist;
            InitializeComponent();
        }

        private void MakeChoiceForm_Load(object sender, EventArgs e)
        {
            System.Collections.ArrayList myAL = new System.Collections.ArrayList() { "dddd", "wwww" };
            FillForm();
            MakeChoiceForm_SizeChanged(sender, e);
        }

        private new void FillForm()
        {
            int i = 1;

            foreach (/*KeyValuePair<int, string>*/string element in _valuelist)
            {
                RadioButton box = new RadioButton();
                box.Text = /*element.Value*/element;
                box.AutoSize = true;
                box.Dock = DockStyle.Fill;
                if (i == 1)
                    box.Checked = true;
                box.Tag = i++/*element.Key*/;

                flpanel.Controls.Add(box);
            }
            
        }

        private void MakeChoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FlowLayoutPanel flpanel = (FlowLayoutPanel)this.Controls[GlobalPanelName].Controls[FlowPanelName];
            if (flpanel != null)
            {

                #region Save_results
                RadioButton rb = new RadioButton();
                foreach (Control cntrl in flpanel.Controls)
                {
                    if (cntrl is RadioButton)
                    {
                        rb = (RadioButton)cntrl;

                        if (rb.Checked == true)
                        {
                            Checked = Convert.ToInt32(rb.Tag);
                            break;
                        }
                    }
                }
                #endregion
               
            }
        }

        private void MakeChoiceForm_SizeChanged(object sender, EventArgs e)
        {
            globalpanel.Left = (this.ClientSize.Width - globalpanel.Width) / 2;
            globalpanel.Top = (this.ClientSize.Height - globalpanel.Height) / 2;

            flpanel.Left = (this.ClientSize.Width - flpanel.Width) / 2;
            flpanel.Top = (this.ClientSize.Height - flpanel.Height) / 2;

            buttonpanel.Left = (this.ClientSize.Width - buttonpanel.Width) / 2;
            buttonpanel.Top = (this.ClientSize.Height - buttonpanel.Height) / 2;        
        }
    }
}
