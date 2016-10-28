namespace CheckConnection
{
    partial class DisplayConnections
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayConnections));
            this.ConnectionsdataGridView = new System.Windows.Forms.DataGridView();
            this.HistorydataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.PingtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.TracerttoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ComparetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ViewComparetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChangeConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRestore = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.HistorybindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ConnectionHistorylabel = new System.Windows.Forms.Label();
            this.HistorybindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionsdataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistorydataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistorybindingNavigator)).BeginInit();
            this.HistorybindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistorybindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectionsdataGridView
            // 
            this.ConnectionsdataGridView.AllowUserToAddRows = false;
            this.ConnectionsdataGridView.AllowUserToDeleteRows = false;
            this.ConnectionsdataGridView.AllowUserToOrderColumns = true;
            this.ConnectionsdataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectionsdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConnectionsdataGridView.Location = new System.Drawing.Point(0, 30);
            this.ConnectionsdataGridView.MultiSelect = false;
            this.ConnectionsdataGridView.Name = "ConnectionsdataGridView";
            this.ConnectionsdataGridView.ReadOnly = true;
            this.ConnectionsdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConnectionsdataGridView.Size = new System.Drawing.Size(786, 128);
            this.ConnectionsdataGridView.TabIndex = 0;
            this.ConnectionsdataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConnectionsdataGridView_RowEnter);
            this.ConnectionsdataGridView.SelectionChanged += new System.EventHandler(this.ConnectionsdataGridView_SelectionChanged);
            // 
            // HistorydataGridView
            // 
            this.HistorydataGridView.AllowUserToAddRows = false;
            this.HistorydataGridView.AllowUserToDeleteRows = false;
            this.HistorydataGridView.AllowUserToOrderColumns = true;
            this.HistorydataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistorydataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistorydataGridView.Location = new System.Drawing.Point(0, 20);
            this.HistorydataGridView.MultiSelect = false;
            this.HistorydataGridView.Name = "HistorydataGridView";
            this.HistorydataGridView.ReadOnly = true;
            this.HistorydataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HistorydataGridView.Size = new System.Drawing.Size(786, 198);
            this.HistorydataGridView.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.ConnectionsdataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.HistorybindingNavigator);
            this.splitContainer1.Panel2.Controls.Add(this.ConnectionHistorylabel);
            this.splitContainer1.Panel2.Controls.Add(this.HistorydataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(786, 386);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PingtoolStripButton,
            this.TracerttoolStripButton,
            this.ComparetoolStripButton,
            this.ViewComparetoolStripButton,
            this.toolStripButtonChangeConnection,
            this.toolStripButtonRestore,
            this.toolStripButtonRefresh,
            this.toolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(786, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // PingtoolStripButton
            // 
            this.PingtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PingtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PingtoolStripButton.Image")));
            this.PingtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PingtoolStripButton.Name = "PingtoolStripButton";
            this.PingtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PingtoolStripButton.Text = "Ping";
            this.PingtoolStripButton.Click += new System.EventHandler(this.PingtoolStripButton_Click);
            // 
            // TracerttoolStripButton
            // 
            this.TracerttoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TracerttoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("TracerttoolStripButton.Image")));
            this.TracerttoolStripButton.ImageTransparentColor = System.Drawing.Color.Orange;
            this.TracerttoolStripButton.Name = "TracerttoolStripButton";
            this.TracerttoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.TracerttoolStripButton.Text = "Tracert";
            this.TracerttoolStripButton.Click += new System.EventHandler(this.TracerttoolStripButton_Click);
            // 
            // ComparetoolStripButton
            // 
            this.ComparetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ComparetoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ComparetoolStripButton.Image")));
            this.ComparetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ComparetoolStripButton.Name = "ComparetoolStripButton";
            this.ComparetoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ComparetoolStripButton.Text = "Compare";
            this.ComparetoolStripButton.ToolTipText = "Сравнить";
            this.ComparetoolStripButton.Click += new System.EventHandler(this.ComparetoolStripButton_Click);
            // 
            // ViewComparetoolStripButton
            // 
            this.ViewComparetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewComparetoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ViewComparetoolStripButton.Image")));
            this.ViewComparetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewComparetoolStripButton.Name = "ViewComparetoolStripButton";
            this.ViewComparetoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ViewComparetoolStripButton.Text = "View Compare";
            this.ViewComparetoolStripButton.ToolTipText = "Сравнить поэлементно";
            this.ViewComparetoolStripButton.Click += new System.EventHandler(this.ViewComparetoolStripButton_Click);
            // 
            // toolStripButtonChangeConnection
            // 
            this.toolStripButtonChangeConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChangeConnection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChangeConnection.Image")));
            this.toolStripButtonChangeConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChangeConnection.Name = "toolStripButtonChangeConnection";
            this.toolStripButtonChangeConnection.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonChangeConnection.Text = "ChangeConnectionParam";
            this.toolStripButtonChangeConnection.ToolTipText = "Изменить параметры подключения";
            this.toolStripButtonChangeConnection.Click += new System.EventHandler(this.toolStripButtonChangeConnection_Click);
            // 
            // toolStripButtonRestore
            // 
            this.toolStripButtonRestore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRestore.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRestore.Image")));
            this.toolStripButtonRestore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRestore.Name = "toolStripButtonRestore";
            this.toolStripButtonRestore.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRestore.Text = "RestoreConnectionParam";
            this.toolStripButtonRestore.ToolTipText = "Восстановить параметры из истории";
            this.toolStripButtonRestore.Click += new System.EventHandler(this.toolStripButtonRestore_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.ToolTipText = "Обновить отображаемые параметры";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // HistorybindingNavigator
            // 
            this.HistorybindingNavigator.AddNewItem = null;
            this.HistorybindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.HistorybindingNavigator.DeleteItem = null;
            this.HistorybindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.HistorybindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.HistorybindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.HistorybindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.HistorybindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.HistorybindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.HistorybindingNavigator.Name = "HistorybindingNavigator";
            this.HistorybindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.HistorybindingNavigator.Size = new System.Drawing.Size(786, 25);
            this.HistorybindingNavigator.TabIndex = 3;
            this.HistorybindingNavigator.Text = "HistorybindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ConnectionHistorylabel
            // 
            this.ConnectionHistorylabel.AutoSize = true;
            this.ConnectionHistorylabel.Location = new System.Drawing.Point(4, 4);
            this.ConnectionHistorylabel.Name = "ConnectionHistorylabel";
            this.ConnectionHistorylabel.Size = new System.Drawing.Size(120, 13);
            this.ConnectionHistorylabel.TabIndex = 2;
            this.ConnectionHistorylabel.Text = "История подключений";
            // 
            // HistorybindingSource
            // 
            this.HistorybindingSource.CurrentChanged += new System.EventHandler(this.HistorybindingSource_CurrentChanged);
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton.Image")));
            this.toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton.Text = "toolStripDropDownButton1";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // DisplayConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 413);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayConnections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Сетевые подключения";
            this.Load += new System.EventHandler(this.DisplayConnections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionsdataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistorydataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistorybindingNavigator)).EndInit();
            this.HistorybindingNavigator.ResumeLayout(false);
            this.HistorybindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistorybindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConnectionsdataGridView;
        private System.Windows.Forms.DataGridView HistorydataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label ConnectionHistorylabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton PingtoolStripButton;
        private System.Windows.Forms.ToolStripButton TracerttoolStripButton;
        private System.Windows.Forms.ToolStripButton ComparetoolStripButton;
        private System.Windows.Forms.ToolStripButton ViewComparetoolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonChangeConnection;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonRestore;
        private System.Windows.Forms.BindingNavigator HistorybindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource HistorybindingSource;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}