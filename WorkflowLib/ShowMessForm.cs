using System;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class ShowMessForm : Form
    {
        readonly string FlowPanelName   = "FlowPanel";
        readonly string ButtonPanelName = "ButtonPanel";
        readonly string GlobalPanelName = "GlobalPanel";
        private string _text = String.Empty;
        public ShowMessForm(string text)
        {
            _text = text;
            InitializeComponent();
        }

        private FlowLayoutPanel FillForm()
        {
            FlowLayoutPanel globalpanel = new FlowLayoutPanel();

            #region Panel                
            globalpanel.FlowDirection = FlowDirection.TopDown;
            globalpanel.AutoSize = true;
            globalpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            globalpanel.WrapContents = true;
            globalpanel.Dock = DockStyle.Fill;
            globalpanel.Name = GlobalPanelName;
            #endregion
            //--------------------------------------------------

            FlowLayoutPanel flpanel = new FlowLayoutPanel();

            #region Panel                
            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;
            flpanel.Name = FlowPanelName;
            #endregion

            Label Textlabel = new Label();
            Textlabel.AutoSize = true;            
            Textlabel.Dock = DockStyle.Fill;
            Textlabel.Text = _text;
            flpanel.Controls.Add(Textlabel);
           
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

        private void ShowMessForm_Load(object sender, EventArgs e)
        {
            this.Controls.Add(FillForm());
        }
    }
}