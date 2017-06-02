namespace ChLauncherWin
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxRegim.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRegim
            // 
            this.groupBoxRegim.Controls.Add(this.radioButtonDiagnose);
            this.groupBoxRegim.Controls.Add(this.radioButtonRepair);
            this.groupBoxRegim.Location = new System.Drawing.Point(12, 12);
            this.groupBoxRegim.Name = "groupBoxRegim";
            this.groupBoxRegim.Size = new System.Drawing.Size(245, 130);
            this.groupBoxRegim.TabIndex = 0;
            this.groupBoxRegim.TabStop = false;
            this.groupBoxRegim.Text = "Режимы работы";
            // 
            // radioButtonDiagnose
            // 
            this.radioButtonDiagnose.AutoSize = true;
            this.radioButtonDiagnose.Location = new System.Drawing.Point(15, 75);
            this.radioButtonDiagnose.Name = "radioButtonDiagnose";
            this.radioButtonDiagnose.Size = new System.Drawing.Size(164, 30);
            this.radioButtonDiagnose.TabIndex = 1;
            this.radioButtonDiagnose.Text = "Расширенная диагностика \r\nинтернет подключения";
            this.radioButtonDiagnose.UseVisualStyleBackColor = true;
            // 
            // radioButtonRepair
            // 
            this.radioButtonRepair.AutoSize = true;
            this.radioButtonRepair.Checked = true;
            this.radioButtonRepair.Location = new System.Drawing.Point(15, 32);
            this.radioButtonRepair.Name = "radioButtonRepair";
            this.radioButtonRepair.Size = new System.Drawing.Size(215, 17);
            this.radioButtonRepair.TabIndex = 0;
            this.radioButtonRepair.TabStop = true;
            this.radioButtonRepair.Text = "Восстановить интернет подключение";
            this.radioButtonRepair.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Выбрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 197);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBoxRegim);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(285, 235);
            this.MinimumSize = new System.Drawing.Size(285, 235);
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мастер диагностики";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxRegim.ResumeLayout(false);
            this.groupBoxRegim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRegim;
        private System.Windows.Forms.RadioButton radioButtonDiagnose;
        private System.Windows.Forms.RadioButton radioButtonRepair;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

