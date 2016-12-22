using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class MakeChoiceForm : Form
    {
        private string[] /*List<KeyValuePair<int, string>>*/ _valuelist;
        readonly string FlowPanelName = "FlowPanel";
        readonly string ButtonPanelName = "ButtonPanel";
        readonly string GlobalPanelName = "GlobalPanelName";
        public int Checked = 0;
        public MakeChoiceForm(string[] /*List<KeyValuePair<int, string>>*/ valuelist)
        {
            _valuelist = valuelist;
            InitializeComponent();
        }

        private void MakeChoiceForm_Load(object sender, EventArgs e)
        {
            System.Collections.ArrayList myAL = new System.Collections.ArrayList() { "dddd", "wwww" };
            this.Controls.Add(FillForm());
        }

        private FlowLayoutPanel FillForm()
        {
            int i = 1;

            FlowLayoutPanel globalpanel = new FlowLayoutPanel();

            #region Panel                
            globalpanel.FlowDirection = FlowDirection.TopDown;
            globalpanel.AutoSize = true;
            globalpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            globalpanel.WrapContents = true;
            globalpanel.Dock = DockStyle.Fill;
            globalpanel.Name = GlobalPanelName;
            #endregion

            FlowLayoutPanel flpanel = new FlowLayoutPanel();

            #region Panel                
            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;
            flpanel.Name = FlowPanelName;            
            #endregion

            foreach (/*KeyValuePair<int, string>*/string element in _valuelist)
            {
                RadioButton box = new RadioButton();
                box.Text = /*element.Value*/element;
                box.AutoSize = true;
                box.Dock = DockStyle.Fill;
                box.Tag = i++/*element.Key*/;
                flpanel.Controls.Add(box);
            }

            //-------------------------------------------------

            FlowLayoutPanel buttonpanel = new FlowLayoutPanel();

            #region Panel                
            buttonpanel.FlowDirection = FlowDirection.LeftToRight;
            buttonpanel.AutoSize = true;
            buttonpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonpanel.WrapContents = true;
            buttonpanel.Dock = DockStyle.Fill;
            buttonpanel.Name = ButtonPanelName;
            #endregion
           

            Button OkButton = new Button();
            OkButton.Text = "Подтвердить";   
            OkButton.DialogResult = DialogResult.OK;

            OkButton.AutoSize = true;
            OkButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;      
            OkButton.Dock = DockStyle.Fill;

            buttonpanel.Controls.Add(OkButton);
            

            Button CancelButton = new Button();
            CancelButton.Text = "Отменить";
            CancelButton.DialogResult = DialogResult.Cancel;

            CancelButton.AutoSize = true;
            CancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton.Dock = DockStyle.Fill;            

            buttonpanel.Controls.Add(CancelButton);

            globalpanel.Controls.Add(flpanel);
            globalpanel.Controls.Add(buttonpanel);

            return globalpanel;
        }

        private void MakeChoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FlowLayoutPanel flpanel = (FlowLayoutPanel)this.Controls[GlobalPanelName].Controls[FlowPanelName];
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
