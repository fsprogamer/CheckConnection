namespace CheckConnection
{
    partial class ChangeConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxIPType = new System.Windows.Forms.GroupBox();
            this.radioButtonStaticIP = new System.Windows.Forms.RadioButton();
            this.radioButtonDHCP = new System.Windows.Forms.RadioButton();
            this.groupBoxIPAddress = new System.Windows.Forms.GroupBox();
            this.labelDNSDomain = new System.Windows.Forms.Label();
            this.textBoxDNSDomain = new System.Windows.Forms.TextBox();
            this.labelDHCPServer = new System.Windows.Forms.Label();
            this.DHCPServerControl = new IPAddressControlLib.IPAddressControl();
            this.labelNetMask = new System.Windows.Forms.Label();
            this.DNSControl2 = new IPAddressControlLib.IPAddressControl();
            this.DNSControl1 = new IPAddressControlLib.IPAddressControl();
            this.labelDNS = new System.Windows.Forms.Label();
            this.GatewayControl2 = new IPAddressControlLib.IPAddressControl();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.ipAddressControl = new IPAddressControlLib.IPAddressControl();
            this.NetMaskControl = new IPAddressControlLib.IPAddressControl();
            this.GatewayControl1 = new IPAddressControlLib.IPAddressControl();
            this.labelGateway = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBoxIPType.SuspendLayout();
            this.groupBoxIPAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.94455F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.05545F));
            this.tableLayoutPanel.Controls.Add(this.buttonSave, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.groupBoxIPType, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.groupBoxIPAddress, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.31658F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.68342F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(572, 287);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(48, 222);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxIPType
            // 
            this.groupBoxIPType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxIPType.Controls.Add(this.radioButtonStaticIP);
            this.groupBoxIPType.Controls.Add(this.radioButtonDHCP);
            this.groupBoxIPType.Location = new System.Drawing.Point(3, 3);
            this.groupBoxIPType.Name = "groupBoxIPType";
            this.groupBoxIPType.Size = new System.Drawing.Size(165, 175);
            this.groupBoxIPType.TabIndex = 0;
            this.groupBoxIPType.TabStop = false;
            this.groupBoxIPType.Text = "Тип IP-адреса";
            // 
            // radioButtonStaticIP
            // 
            this.radioButtonStaticIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonStaticIP.AutoSize = true;
            this.radioButtonStaticIP.Location = new System.Drawing.Point(45, 90);
            this.radioButtonStaticIP.Name = "radioButtonStaticIP";
            this.radioButtonStaticIP.Size = new System.Drawing.Size(65, 17);
            this.radioButtonStaticIP.TabIndex = 2;
            this.radioButtonStaticIP.Text = "Static IP";
            this.radioButtonStaticIP.UseVisualStyleBackColor = true;
            // 
            // radioButtonDHCP
            // 
            this.radioButtonDHCP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonDHCP.AutoSize = true;
            this.radioButtonDHCP.Checked = true;
            this.radioButtonDHCP.Location = new System.Drawing.Point(45, 67);
            this.radioButtonDHCP.Name = "radioButtonDHCP";
            this.radioButtonDHCP.Size = new System.Drawing.Size(55, 17);
            this.radioButtonDHCP.TabIndex = 1;
            this.radioButtonDHCP.TabStop = true;
            this.radioButtonDHCP.Text = "DHCP";
            this.radioButtonDHCP.UseVisualStyleBackColor = true;
            this.radioButtonDHCP.CheckedChanged += new System.EventHandler(this.radioButtonDHCP_CheckedChanged);
            // 
            // groupBoxIPAddress
            // 
            this.groupBoxIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxIPAddress.Controls.Add(this.labelDNSDomain);
            this.groupBoxIPAddress.Controls.Add(this.textBoxDNSDomain);
            this.groupBoxIPAddress.Controls.Add(this.labelDHCPServer);
            this.groupBoxIPAddress.Controls.Add(this.DHCPServerControl);
            this.groupBoxIPAddress.Controls.Add(this.labelNetMask);
            this.groupBoxIPAddress.Controls.Add(this.DNSControl2);
            this.groupBoxIPAddress.Controls.Add(this.DNSControl1);
            this.groupBoxIPAddress.Controls.Add(this.labelDNS);
            this.groupBoxIPAddress.Controls.Add(this.GatewayControl2);
            this.groupBoxIPAddress.Controls.Add(this.labelIPAddress);
            this.groupBoxIPAddress.Controls.Add(this.ipAddressControl);
            this.groupBoxIPAddress.Controls.Add(this.NetMaskControl);
            this.groupBoxIPAddress.Controls.Add(this.GatewayControl1);
            this.groupBoxIPAddress.Controls.Add(this.labelGateway);
            this.groupBoxIPAddress.Location = new System.Drawing.Point(174, 3);
            this.groupBoxIPAddress.Name = "groupBoxIPAddress";
            this.groupBoxIPAddress.Size = new System.Drawing.Size(395, 175);
            this.groupBoxIPAddress.TabIndex = 3;
            this.groupBoxIPAddress.TabStop = false;
            this.groupBoxIPAddress.Text = "Параметры подключения";
            // 
            // labelDNSDomain
            // 
            this.labelDNSDomain.AutoSize = true;
            this.labelDNSDomain.Location = new System.Drawing.Point(263, 80);
            this.labelDNSDomain.Name = "labelDNSDomain";
            this.labelDNSDomain.Size = new System.Drawing.Size(66, 13);
            this.labelDNSDomain.TabIndex = 17;
            this.labelDNSDomain.Text = "DNSDomain";
            // 
            // textBoxDNSDomain
            // 
            this.textBoxDNSDomain.Location = new System.Drawing.Point(263, 96);
            this.textBoxDNSDomain.Name = "textBoxDNSDomain";
            this.textBoxDNSDomain.Size = new System.Drawing.Size(123, 20);
            this.textBoxDNSDomain.TabIndex = 16;
            // 
            // labelDHCPServer
            // 
            this.labelDHCPServer.AutoSize = true;
            this.labelDHCPServer.Location = new System.Drawing.Point(260, 20);
            this.labelDHCPServer.Name = "labelDHCPServer";
            this.labelDHCPServer.Size = new System.Drawing.Size(76, 13);
            this.labelDHCPServer.TabIndex = 14;
            this.labelDHCPServer.Text = "DHCP сервер";
            // 
            // DHCPServerControl
            // 
            this.DHCPServerControl.AllowInternalTab = false;
            this.DHCPServerControl.AutoHeight = true;
            this.DHCPServerControl.BackColor = System.Drawing.SystemColors.Window;
            this.DHCPServerControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DHCPServerControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DHCPServerControl.Location = new System.Drawing.Point(263, 36);
            this.DHCPServerControl.MinimumSize = new System.Drawing.Size(87, 20);
            this.DHCPServerControl.Name = "DHCPServerControl";
            this.DHCPServerControl.ReadOnly = false;
            this.DHCPServerControl.Size = new System.Drawing.Size(87, 20);
            this.DHCPServerControl.TabIndex = 15;
            this.DHCPServerControl.Text = "...";
            // 
            // labelNetMask
            // 
            this.labelNetMask.AutoSize = true;
            this.labelNetMask.Location = new System.Drawing.Point(149, 20);
            this.labelNetMask.Name = "labelNetMask";
            this.labelNetMask.Size = new System.Drawing.Size(84, 13);
            this.labelNetMask.TabIndex = 6;
            this.labelNetMask.Text = "Маска подсети";
            // 
            // DNSControl2
            // 
            this.DNSControl2.AllowInternalTab = false;
            this.DNSControl2.AutoHeight = true;
            this.DNSControl2.BackColor = System.Drawing.SystemColors.Window;
            this.DNSControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DNSControl2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DNSControl2.Location = new System.Drawing.Point(149, 137);
            this.DNSControl2.MinimumSize = new System.Drawing.Size(87, 20);
            this.DNSControl2.Name = "DNSControl2";
            this.DNSControl2.ReadOnly = false;
            this.DNSControl2.Size = new System.Drawing.Size(87, 20);
            this.DNSControl2.TabIndex = 13;
            this.DNSControl2.Text = "...";
            // 
            // DNSControl1
            // 
            this.DNSControl1.AllowInternalTab = false;
            this.DNSControl1.AutoHeight = true;
            this.DNSControl1.BackColor = System.Drawing.SystemColors.Window;
            this.DNSControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DNSControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DNSControl1.Location = new System.Drawing.Point(149, 97);
            this.DNSControl1.MinimumSize = new System.Drawing.Size(87, 20);
            this.DNSControl1.Name = "DNSControl1";
            this.DNSControl1.ReadOnly = false;
            this.DNSControl1.Size = new System.Drawing.Size(87, 20);
            this.DNSControl1.TabIndex = 12;
            this.DNSControl1.Text = "...";
            // 
            // labelDNS
            // 
            this.labelDNS.AutoSize = true;
            this.labelDNS.Location = new System.Drawing.Point(149, 80);
            this.labelDNS.Name = "labelDNS";
            this.labelDNS.Size = new System.Drawing.Size(30, 13);
            this.labelDNS.TabIndex = 11;
            this.labelDNS.Text = "DNS";
            // 
            // GatewayControl2
            // 
            this.GatewayControl2.AllowInternalTab = false;
            this.GatewayControl2.AutoHeight = true;
            this.GatewayControl2.BackColor = System.Drawing.SystemColors.Window;
            this.GatewayControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GatewayControl2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GatewayControl2.Location = new System.Drawing.Point(30, 137);
            this.GatewayControl2.MinimumSize = new System.Drawing.Size(87, 20);
            this.GatewayControl2.Name = "GatewayControl2";
            this.GatewayControl2.ReadOnly = false;
            this.GatewayControl2.Size = new System.Drawing.Size(87, 20);
            this.GatewayControl2.TabIndex = 10;
            this.GatewayControl2.Text = "...";
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(29, 20);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(50, 13);
            this.labelIPAddress.TabIndex = 4;
            this.labelIPAddress.Text = "IP-адрес";
            // 
            // ipAddressControl
            // 
            this.ipAddressControl.AllowInternalTab = false;
            this.ipAddressControl.AutoHeight = true;
            this.ipAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl.Location = new System.Drawing.Point(30, 37);
            this.ipAddressControl.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl.Name = "ipAddressControl";
            this.ipAddressControl.ReadOnly = false;
            this.ipAddressControl.Size = new System.Drawing.Size(87, 20);
            this.ipAddressControl.TabIndex = 5;
            this.ipAddressControl.Text = "...";
            // 
            // NetMaskControl
            // 
            this.NetMaskControl.AllowInternalTab = false;
            this.NetMaskControl.AutoHeight = true;
            this.NetMaskControl.BackColor = System.Drawing.SystemColors.Window;
            this.NetMaskControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NetMaskControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NetMaskControl.Location = new System.Drawing.Point(149, 36);
            this.NetMaskControl.MinimumSize = new System.Drawing.Size(87, 20);
            this.NetMaskControl.Name = "NetMaskControl";
            this.NetMaskControl.ReadOnly = false;
            this.NetMaskControl.Size = new System.Drawing.Size(87, 20);
            this.NetMaskControl.TabIndex = 7;
            this.NetMaskControl.Text = "...";
            // 
            // GatewayControl1
            // 
            this.GatewayControl1.AllowInternalTab = false;
            this.GatewayControl1.AutoHeight = true;
            this.GatewayControl1.BackColor = System.Drawing.SystemColors.Window;
            this.GatewayControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GatewayControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GatewayControl1.Location = new System.Drawing.Point(30, 97);
            this.GatewayControl1.MinimumSize = new System.Drawing.Size(87, 20);
            this.GatewayControl1.Name = "GatewayControl1";
            this.GatewayControl1.ReadOnly = false;
            this.GatewayControl1.Size = new System.Drawing.Size(87, 20);
            this.GatewayControl1.TabIndex = 9;
            this.GatewayControl1.Text = "...";
            // 
            // labelGateway
            // 
            this.labelGateway.AutoSize = true;
            this.labelGateway.Location = new System.Drawing.Point(29, 81);
            this.labelGateway.Name = "labelGateway";
            this.labelGateway.Size = new System.Drawing.Size(36, 13);
            this.labelGateway.TabIndex = 8;
            this.labelGateway.Text = "Шлюз";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(334, 222);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ChangeConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 287);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ChangeConnectionForm";
            this.Text = "Изменение параметров подключения";
            this.Load += new System.EventHandler(this.ChangeConnectionForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBoxIPType.ResumeLayout(false);
            this.groupBoxIPType.PerformLayout();
            this.groupBoxIPAddress.ResumeLayout(false);
            this.groupBoxIPAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonDHCP;
        private System.Windows.Forms.GroupBox groupBoxIPType;
        private System.Windows.Forms.RadioButton radioButtonStaticIP;
        private System.Windows.Forms.GroupBox groupBoxIPAddress;
        private System.Windows.Forms.Label labelGateway;
        private System.Windows.Forms.Label labelNetMask;
        private IPAddressControlLib.IPAddressControl DNSControl2;
        private IPAddressControlLib.IPAddressControl DNSControl1;
        private System.Windows.Forms.Label labelDNS;
        private IPAddressControlLib.IPAddressControl GatewayControl2;
        private System.Windows.Forms.Label labelIPAddress;
        private IPAddressControlLib.IPAddressControl ipAddressControl;
        private IPAddressControlLib.IPAddressControl NetMaskControl;
        private IPAddressControlLib.IPAddressControl GatewayControl1;
        private System.Windows.Forms.Label labelDHCPServer;
        private IPAddressControlLib.IPAddressControl DHCPServerControl;
        private System.Windows.Forms.TextBox textBoxDNSDomain;
        private System.Windows.Forms.Label labelDNSDomain;
    }
}