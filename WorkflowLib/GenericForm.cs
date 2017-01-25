using System;
using System.Windows.Forms;

namespace WorkflowLib
{
    public partial class GenericForm : Form
    {
        readonly string FlowPanelName   = "FlowPanel";
        readonly string ButtonPanelName = "ButtonPanel";
        readonly string GlobalPanelName = "GlobalPanel";

        protected FlowLayoutPanel globalpanel = new FlowLayoutPanel();
        protected FlowLayoutPanel flpanel     = new FlowLayoutPanel();
        protected FlowLayoutPanel buttonpanel = new FlowLayoutPanel();

        public GenericForm()
        {
            InitializeComponent();            
            this.Controls.Add(FillForm());
        }

        protected FlowLayoutPanel FillForm()
        {
            #region GlobalPanel                
            globalpanel.FlowDirection = FlowDirection.TopDown;
            globalpanel.AutoSize = true;
            globalpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            globalpanel.WrapContents = true;
            globalpanel.Dock = DockStyle.Fill;
            globalpanel.Name = GlobalPanelName;
            globalpanel.Anchor = AnchorStyles.None;
            #endregion

            //--------------------------------------------------

            #region FlowPanel                
            flpanel.FlowDirection = FlowDirection.TopDown;
            flpanel.AutoSize = true;
            flpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpanel.WrapContents = true;
            flpanel.Dock = DockStyle.Fill;
            flpanel.Name = FlowPanelName;
            flpanel.Anchor = AnchorStyles.None;
            #endregion

            //-------------------------------------------------

            #region ButtonPanel               
            buttonpanel.FlowDirection = FlowDirection.LeftToRight;
            buttonpanel.AutoSize = true;
            buttonpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonpanel.WrapContents = true;
            buttonpanel.Dock = DockStyle.Fill;
            buttonpanel.Name = ButtonPanelName;
            buttonpanel.Anchor = AnchorStyles.None;

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
            #endregion

            globalpanel.Controls.Add(flpanel);
            globalpanel.Controls.Add(buttonpanel);

            return globalpanel;
        }

    }
}
