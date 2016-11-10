namespace CheckConnection
{
    partial class AnalyzeForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.hostIPHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roundTripTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBoxConclusion = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(170, 310);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.AllowColumnReorder = true;
            this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostIPHeader,
            this.hostNameHeader,
            this.statusHeader,
            this.roundTripTimeHeader});
            this.listViewResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.GridLines = true;
            this.listViewResults.Location = new System.Drawing.Point(0, 0);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(436, 195);
            this.listViewResults.TabIndex = 16;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // hostIPHeader
            // 
            this.hostIPHeader.Text = "Узел";
            this.hostIPHeader.Width = 120;
            // 
            // hostNameHeader
            // 
            this.hostNameHeader.Text = "Имя узла";
            this.hostNameHeader.Width = 122;
            // 
            // statusHeader
            // 
            this.statusHeader.Text = "Статус";
            this.statusHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.statusHeader.Width = 111;
            // 
            // roundTripTimeHeader
            // 
            this.roundTripTimeHeader.Text = "Время";
            this.roundTripTimeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.roundTripTimeHeader.Width = 82;
            // 
            // listBoxConclusion
            // 
            this.listBoxConclusion.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBoxConclusion.FormattingEnabled = true;
            this.listBoxConclusion.HorizontalScrollbar = true;
            this.listBoxConclusion.Items.AddRange(new object[] {
            "Идет анализ подключения "});
            this.listBoxConclusion.Location = new System.Drawing.Point(0, 195);
            this.listBoxConclusion.Name = "listBoxConclusion";
            this.listBoxConclusion.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxConclusion.Size = new System.Drawing.Size(436, 95);
            this.listBoxConclusion.TabIndex = 17;
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 345);
            this.Controls.Add(this.listBoxConclusion);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.buttonClose);
            this.MaximumSize = new System.Drawing.Size(452, 383);
            this.MinimumSize = new System.Drawing.Size(452, 383);
            this.Name = "AnalyzeForm";
            this.Text = "Анализ подключения";
            this.Load += new System.EventHandler(this.AnalyzeForm_Load);
            this.Shown += new System.EventHandler(this.AnalyzeForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ColumnHeader hostIPHeader;
        private System.Windows.Forms.ColumnHeader statusHeader;
        private System.Windows.Forms.ColumnHeader hostNameHeader;
        private System.Windows.Forms.ColumnHeader roundTripTimeHeader;
        private System.Windows.Forms.ListBox listBoxConclusion;
    }
}