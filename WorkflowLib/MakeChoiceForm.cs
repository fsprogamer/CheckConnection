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
    }
}
