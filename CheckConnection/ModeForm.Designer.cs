namespace CheckConnection
{
    partial class ModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeForm));
            this.groupBoxRegim = new System.Windows.Forms.GroupBox();
            this.radioButtonDiagnose = new System.Windows.Forms.RadioButton();
            this.radioButtonRepair = new System.Windows.Forms.RadioButton();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxRegim.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRegim
            // 
            this.groupBoxRegim.Controls.Add(this.radioButtonDiagnose);
            this.groupBoxRegim.Controls.Add(this.radioButtonRepair);
            this.groupBoxRegim.Location = new System.Drawing.Point(14, 14);
            this.groupBoxRegim.Name = "groupBoxRegim";
            this.groupBoxRegim.Size = new System.Drawing.Size(286, 150);
            this.groupBoxRegim.TabIndex = 0;
            this.groupBoxRegim.TabStop = false;
            this.groupBoxRegim.Text = "Режимы работы";
            // 
            // radioButtonDiagnose
            // 
            this.radioButtonDiagnose.AutoSize = true;
            this.radioButtonDiagnose.Location = new System.Drawing.Point(17, 87);
            this.radioButtonDiagnose.Name = "radioButtonDiagnose";
            this.radioButtonDiagnose.Size = new System.Drawing.Size(175, 34);
            this.radioButtonDiagnose.TabIndex = 1;
            this.radioButtonDiagnose.Text = "Расширенная диагностика \r\nинтернет подключения";
            this.radioButtonDiagnose.UseVisualStyleBackColor = true;
            // 
            // radioButtonRepair
            // 
            this.radioButtonRepair.AutoSize = true;
            this.radioButtonRepair.Checked = true;
            this.radioButtonRepair.Location = new System.Drawing.Point(17, 37);
            this.radioButtonRepair.Name = "radioButtonRepair";
            this.radioButtonRepair.Size = new System.Drawing.Size(232, 19);
            this.radioButtonRepair.TabIndex = 0;
            this.radioButtonRepair.TabStop = true;
            this.radioButtonRepair.Text = "Восстановить интернет подключение";
            this.radioButtonRepair.UseVisualStyleBackColor = true;
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(31, 187);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(87, 27);
            this.buttonChoose.TabIndex = 2;
            this.buttonChoose.Text = "Выбрать";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(195, 187);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // ModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(314, 227);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxRegim);
            this.Controls.Add(this.buttonChoose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(330, 265);
            this.MinimumSize = new System.Drawing.Size(330, 265);
            this.Name = "ModeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мастер диагностики";
            this.groupBoxRegim.ResumeLayout(false);
            this.groupBoxRegim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRegim;
        private System.Windows.Forms.RadioButton radioButtonDiagnose;
        private System.Windows.Forms.RadioButton radioButtonRepair;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonCancel;
    }
}

