namespace PingForm
{
    partial class MainPingForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.close = new System.Windows.Forms.Button();
            this.pingList = new System.Windows.Forms.ListView();
            this.hostIPHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hopHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roundTripTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startPing = new System.Windows.Forms.Button();
            this.destination = new System.Windows.Forms.TextBox();
            this.tracert = new VRK.Net.Tracert();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 15);
            label2.TabIndex = 16;
            label2.Text = "&Результат:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 14);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(119, 15);
            label1.TabIndex = 12;
            label1.Text = "&Адрес или имя узла:";
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(369, 320);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(87, 27);
            this.close.TabIndex = 17;
            this.close.Text = "&Закрыть";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pingList
            // 
            this.pingList.AllowColumnReorder = true;
            this.pingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostIPHeader,
            this.hopHeader,
            this.hostNameHeader,
            this.roundTripTimeHeader});
            this.pingList.GridLines = true;
            this.pingList.Location = new System.Drawing.Point(14, 62);
            this.pingList.Name = "pingList";
            this.pingList.Size = new System.Drawing.Size(441, 250);
            this.pingList.TabIndex = 15;
            this.pingList.UseCompatibleStateImageBehavior = false;
            this.pingList.View = System.Windows.Forms.View.Details;
            // 
            // hostIPHeader
            // 
            this.hostIPHeader.DisplayIndex = 1;
            this.hostIPHeader.Text = "Узел";
            this.hostIPHeader.Width = 119;
            // 
            // hopHeader
            // 
            this.hopHeader.DisplayIndex = 0;
            this.hopHeader.Text = "Переход";
            this.hopHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hopHeader.Width = 56;
            // 
            // hostNameHeader
            // 
            this.hostNameHeader.Text = "Имя узла";
            this.hostNameHeader.Width = 141;
            // 
            // roundTripTimeHeader
            // 
            this.roundTripTimeHeader.Text = "Время";
            this.roundTripTimeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.roundTripTimeHeader.Width = 62;
            // 
            // startPing
            // 
            this.startPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startPing.Location = new System.Drawing.Point(369, 9);
            this.startPing.Name = "startPing";
            this.startPing.Size = new System.Drawing.Size(87, 27);
            this.startPing.TabIndex = 14;
            this.startPing.Text = "&Начать пинг";
            this.startPing.UseVisualStyleBackColor = true;
            this.startPing.Click += new System.EventHandler(this.startPing_Click);
            // 
            // destination
            // 
            this.destination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destination.Location = new System.Drawing.Point(150, 10);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(198, 23);
            this.destination.TabIndex = 13;
            // 
            // tracert
            // 
            this.tracert.HostNameOrAddress = null;
            this.tracert.MaxHops = 30;
            this.tracert.TimeOut = 5000;
            this.tracert.Done += new System.EventHandler(this.tracert_Done);
            // 
            // MainPingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 357);
            this.Controls.Add(this.close);
            this.Controls.Add(label2);
            this.Controls.Add(this.pingList);
            this.Controls.Add(this.startPing);
            this.Controls.Add(this.destination);
            this.Controls.Add(label1);
            this.Name = "MainPingForm";
            this.Text = "Ping узла";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ListView pingList;
        private System.Windows.Forms.ColumnHeader hostIPHeader;
        private System.Windows.Forms.ColumnHeader hopHeader;
        private System.Windows.Forms.ColumnHeader hostNameHeader;
        private System.Windows.Forms.ColumnHeader roundTripTimeHeader;
        private System.Windows.Forms.Button startPing;
        private System.Windows.Forms.TextBox destination;
        private VRK.Net.Tracert tracert;
    }
}

