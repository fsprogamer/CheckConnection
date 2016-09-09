namespace CheckConnection
{
    partial class CompareConnections
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
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.AllowUserToOrderColumns = true;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.Location = new System.Drawing.Point(0, 3);
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.Size = new System.Drawing.Size(636, 316);
            this.resultDataGridView.TabIndex = 4;
            this.resultDataGridView.SizeChanged += new System.EventHandler(this.resultDataGridView_SizeChanged);
            // 
            // CompareConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 322);
            this.Controls.Add(this.resultDataGridView);
            this.Name = "CompareConnections";
            this.Text = "Сравнение подключений";
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultDataGridView;
    }
}